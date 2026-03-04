using System;

namespace ConsoleApp2
{
    internal class Program
    {
        public enum TicketType
        {
            Standard,
            VIP,
            IMAX
        }

        public struct Seat
        {
            public char Row { get; set; }
            public int Number { get; set; }

            public Seat(char row, int number)
            {
                Row = row;
                Number = number;
            }

            public override string ToString()
            {
                return $"{Row}{Number}";
            }
        }

        public class Ticket
        {
            private static int ticketCounter = 0;
            private string movieName;
            private double price;

            public string MovieName
            {
                get => movieName;
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                        movieName = value;
                }
            }
            public TicketType Type { get; set; }
            public Seat Seat { get; set; }
            public double Price
            {
                get
                {
                    return price;
                }
                set
                {
                    if (value > 0)
                        price = value;
                }
            }

            public double PriceAfterTax => Price * 1.14;
            public int TicketId { get; private set; }
            public Ticket(string movieName, TicketType type, Seat seat, double price)
            {
                ticketCounter++;
                TicketId = ticketCounter;

                MovieName = movieName;
                Type = type;
                Seat = seat;
                Price = price;
            }
            public static int GetTotalTicketsSold()
            {
                return ticketCounter;
            }
        }

        public class Cinema
        {
            private Ticket[] tickets = new Ticket[20];

            public Ticket this[int index]
            {
                get
                {
                    if (index >= 0 && index < tickets.Length)
                        return tickets[index];
                    return null;
                }
                set
                {
                    if (index >= 0 && index < tickets.Length)
                        tickets[index] = value;
                }
            }

            public Ticket GetMovie(string movieName)
            {
                foreach (var ticket in tickets)
                {
                    if (ticket != null && ticket.MovieName == movieName)
                        return ticket;
                }
                return null;
            }

            public bool AddTicket(Ticket t)
            {
                for (int i = 0; i < tickets.Length; i++)
                {
                    if (tickets[i] == null)
                    {
                        tickets[i] = t;
                        return true;
                    }
                }
                return false;
            }
        }

        public static class BookingHelper
        {
            private static int bookingCounter = 0;

            public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
            {
                double total = numberOfTickets * pricePerTicket;

                if (numberOfTickets >= 5)
                    return total * 0.9; // 10% discount

                return total;
            }

            public static string GenerateBookingReference()
            {
                bookingCounter++;
                return $"BK-{bookingCounter}";
            }
        }

        static void Main(string[] args)
        {
            Cinema cinema = new Cinema();

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"\nEnter Data For Ticket {i + 1}");

                Console.Write("Movie Name: ");
                string movie = Console.ReadLine();

                Console.Write("Ticket Type (0=Standard,1=VIP,2=IMAX): ");
                TicketType type = (TicketType)int.Parse(Console.ReadLine());

                Console.Write("Seat Row: ");
                char row = char.Parse(Console.ReadLine());

                Console.Write("Seat Number: ");
                int number = int.Parse(Console.ReadLine());

                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine());

                Ticket ticket = new Ticket(movie, type, new Seat(row, number), price);
                cinema.AddTicket(ticket);
            }

            Console.WriteLine("\n===== All Tickets =====");
            for (int i = 0; i < 3; i++)
            {
                Ticket t = cinema[i];
                if (t != null)
                {
                    Console.WriteLine($"ID: {t.TicketId}");
                    Console.WriteLine($"Movie: {t.MovieName}");
                    Console.WriteLine($"Type: {t.Type}");
                    Console.WriteLine($"Seat: {t.Seat}");
                    Console.WriteLine($"Price: {t.Price}");
                    Console.WriteLine($"Price After Tax: {t.PriceAfterTax:F2}");
                    Console.WriteLine();
                }
            }

            Console.Write("Enter movie name to search: ");
            string searchName = Console.ReadLine();

            Ticket found = cinema.GetMovie(searchName);

            if (found != null)
                Console.WriteLine($"Found Ticket ID: {found.TicketId}");
            else
                Console.WriteLine("Movie not found.");

            Console.WriteLine($"\nTotal Tickets Sold: {Ticket.GetTotalTicketsSold()}");
            Console.WriteLine($"Booking Ref 1: {BookingHelper.GenerateBookingReference()}");
            Console.WriteLine($"Booking Ref 2: {BookingHelper.GenerateBookingReference()}");
            double groupPrice = BookingHelper.CalcGroupDiscount(5, 80);
            Console.WriteLine($"\nGroup price for 5 tickets (80 EGP each): {groupPrice}");
        }
    }
}