using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compsci
{
    internal class Program
    {
        
        const float gst = 0.1f;
        public static bool paymnt;
        public static bool ordercomp = false;
        public static bool disptlt = false;
        static void Main(string[] args)
        {
            int[,] custorder = null;
            
            double total = 0;
            
            string[,] items = new string[4, 3]
            
            {
            { "Cappuccino", "4.12", "0" }, //<item>,<price as float>, <total quantity sold>
            { "Espresso", "3.22", "0" },
            { "Latte", "3.51", "0" },
            { "Iced","3.57", "0" }
            };
            
           string[] apps = { "0- New Order", "1- Display total", "2- Payment", "3- Display Total QTY Sold", "4- EXIT"};
            while (true)
            {
                for (int i = 0; i < apps.GetLength(0); i++)
                {
                    if (ordercomp == true || i == 3 || i == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.WriteLine(apps[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        
                    }
                    else if (ordercomp == false && i !=4 )
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine(apps[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }

                Console.WriteLine("Select with the number of the option you want to select");
                Console.Write("Number:");

                int input = Int32.Parse(Console.ReadLine());
                Console.Clear();

                switch (input)
                {
                    case 0:
                        custorder = order(ref items);
                        break;

                    case 1:
                        if (ordercomp == true)
                        {
                            total = CalcDispTotal(custorder, items, gst);
                            Console.WriteLine("Total: " + total);
                        }
                        else if (ordercomp == false)
                        {
                            Console.WriteLine("You have not placed an order");
                        }

                        break;

                    case 2:
                        if (ordercomp == true && disptlt == true)
                        {
                            PayProcess(total);
                        }
                        else if (ordercomp == false)
                        {
                            Console.WriteLine("You have not placed an order");
                        }

                        break;

                    case 3:
                        Dissales(items);

                        break;
                    case 4:
                        Environment.Exit(0);
                        break;

                }
                
            }
            

            
            
            //int [,] custorder = order(ref items);


            //total = CalcDispTotal(custorder, items, gst);

            //PayProcess(total);
            
            //Dissales(items);
            
            //Console.ReadLine();



        }
        
        
        static int[,] order(ref string[,] items)
        {
            string item;
            int qtyint = 1;
            string qtystr;
            int linenum = 0;
            Console.WriteLine("Welcome to the coffee shop");
            Console.WriteLine("Please enter your order from the avaliable items");

            int[,] order = new int[4, 2] { {0,0}, {1,0}, {2,0}, {3,0} };


            do
            {
                
                qtyint = 0;
                
                //print current order
                Console.Clear();
                Console.WriteLine("↓-------------Current Order-------------↓");
                for (int i = 0; i < order.GetLength(0); i++)
                {
                    if (order[i, 1] != 0)
                    {
                        Console.WriteLine("{0} - x{1} ()", items[order[i, 0], 0], order[i, 1]);
                    }
                }
                Console.WriteLine("↑-------------Current Order-------------↑");
                //print current order
                

                
                
                // print avaliable items
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    Console.WriteLine("{0} - {1} (${2})", i, items[i, 0], items[i, 1]);
                }

                Console.WriteLine("Enter the item number or hit Enter to finish order:");
                Console.Write("Item:"); //SELECT ITEM'
                item = Console.ReadLine();
                //print avaliable items

                


                
                Console.Clear();
                if (item != "")
                {
                    Console.WriteLine("↓-------------Current Order-------------↓");
                    for (int i = 0; i < order.GetLength(0); i++)
                    {
                        if (order[i, 1] != 0)
                        {
                            if (i == Int32.Parse(item))
                            {
                                Console.WriteLine("[{0}] - x{1} ()", items[order[i, 0], 0], order[i, 1]);
                            }
                            else
                            {
                                Console.WriteLine("{0} - x{1} ()", items[order[i, 0], 0], order[i, 1]);
                            }
                        }
                    }
                    Console.WriteLine("↑-------------Current Order-------------↑");



                    Console.WriteLine("Enter the quantity for [{0}], press enter for Qty of 1:", items[Int32.Parse(item), 0]);
                    Console.Write("Qty:");
                    qtystr = Console.ReadLine();
                    if (qtystr != "")
                    {
                        qtyint = Int32.Parse(qtystr);
                    }
                    else
                    {
                        qtyint = 1;
                    }


                    if (item != "")
                    {
                        items[Int32.Parse(item), 2] = items[Int32.Parse(item), 2] + qtyint;
                        order[Int32.Parse(item), 0] = Int32.Parse(item);
                        order[Int32.Parse(item), 1] = qtyint;
                        linenum++;
                    }
                }

            } while (item != "");
            ordercomp = true;
            return order;
        }

        static double CalcDispTotal(int[,] order, string[,] items, float gst)
        {
            double total = 0;
            Console.WriteLine("↓-------------Your Order-------------↓");
            for (int i = 0; i < order.GetLength(0); i++)
            {
                if (order[i, 1] != 0)
                {
                    Console.WriteLine("{0} - x{1} @${2} (${3})", items[order[i, 0], 0], order[i, 1], items[i, 1], float.Parse(items[order[i, 0], 1]) * order[i, 1]);
                    total = total + float.Parse(items[order[i, 0], 1]) * order[i, 1];
                }
                
            }
            total = Math.Round(total * 20) / 20 ;
            Console.WriteLine("↑-------------Your Order-------------↑");
            Console.WriteLine("GST: ${0}", total * gst);
            Console.WriteLine("TOTAL: ${0}", total);
            paymnt = true;
            disptlt = true;
            return total;
        }

        static void PayProcess(double total)
        {
            double paymentin;
            do
            {
                Console.WriteLine("↓Amount Due↓");
                Console.WriteLine("Total: ${0}", total);
                Console.WriteLine("Enter the change-in amount");
                Console.Write("$:");
                paymentin = Convert.ToDouble(Console.ReadLine());
                total = total - paymentin;

                if (total > 0)
                {
                    Console.Clear();
                    Console.WriteLine("↓The amount you entered is less than the total↓");
                    Console.WriteLine("Total: ${0}", total);
                    Console.WriteLine("Enter the change-in amount");
                    Console.Write("$:");
                }
                else if (total <= 0)
                {
                    if (total < 0)
                    {
                        total = total * (-1);
                    }
                    Console.WriteLine("Change:${0}",  total);
                    Console.WriteLine("-Press Enter to Continue-");
                    Console.ReadLine();
                }
            } while (total >= 0);
        }
        
        static void Dissales(string[,] items)
        {
            Console.WriteLine("↓TOTAL QTY OF ITEMS SOLD↓");
            for (int i = 0; i < items.GetLength(0); i++)
            {
                Console.WriteLine("{0} ({1})", items[i, 0], items[i, 2]);
            }
            Console.WriteLine("↑TOTAL QTY OF ITEMS SOLD↑");
        }
    }
}
