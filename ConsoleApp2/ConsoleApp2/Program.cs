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

        #region Ticket Base Class
        public class Ticket
        {
            private static int ticketCounter = 0;
            private string movieName;
            private double price;

            public int TicketId { get; private set; }

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
                get => price;
                set
                {
                    if (value > 0)
                        price = value;
                }
            }

            public Ticket(string movieName, TicketType type, Seat seat, double price)
            {
                ticketCounter++;
                TicketId = ticketCounter;

                MovieName = movieName;
                Type = type;
                Seat = seat;
                Price = price;
            }

            public override string ToString()
            {
                return $"Ticket ID: {TicketId}\nMovie: {MovieName}\nType: {Type}\nSeat: {Seat}\nPrice: {Price}";
            }
        }
        #endregion

        #region Child Classes

        public class StandardTicket : Ticket
        {
            public string SeatNumber { get; set; }

            public StandardTicket(string movie, Seat seat, double price, string seatNumber)
                : base(movie, TicketType.Standard, seat, price)
            {
                SeatNumber = seatNumber;
            }

            public override string ToString()
            {
                return base.ToString() + $"\nSeat Number: {SeatNumber}\n";
            }
        }

        public class VIPTicket : Ticket
        {
            public bool LoungeAccess { get; set; }
            public decimal ServiceFee { get; set; } = 50;

            public VIPTicket(string movie, Seat seat, double price, bool loungeAccess)
                : base(movie, TicketType.VIP, seat, price)
            {
                LoungeAccess = loungeAccess;
            }

            public override string ToString()
            {
                return base.ToString() +
                       $"\nLounge Access: {LoungeAccess}\nService Fee: {ServiceFee}\n";
            }
        }

        public class IMAXTicket : Ticket
        {
            public bool Is3D { get; set; }

            public IMAXTicket(string movie, Seat seat, double price, bool is3D)
                : base(movie, TicketType.IMAX, seat, is3D ? price + 30 : price)
            {
                Is3D = is3D;
            }

            public override string ToString()
            {
                return base.ToString() + $"\n3D: {Is3D}\n";
            }
        }

        #endregion

        #region Projector Class
        public class Projector
        {
            public void Start()
            {
                Console.WriteLine("Projector started.");
            }

            public void Stop()
            {
                Console.WriteLine("Projector stopped.");
            }
        }
        #endregion

        #region Cinema Class
        public class Cinema
        {
            public string CinemaName { get; set; }

            private Ticket[] tickets = new Ticket[20];
            private Projector projector = new Projector();

            public Cinema(string name)
            {
                CinemaName = name;
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

            public void PrintAllTickets()
            {
                Console.WriteLine($"\nTickets in {CinemaName}\n");

                foreach (var ticket in tickets)
                {
                    if (ticket != null)
                        Console.WriteLine(ticket);
                }
            }

            public void OpenCinema()
            {
                Console.WriteLine($"{CinemaName} is now OPEN.");
                projector.Start();
            }

            public void CloseCinema()
            {
                Console.WriteLine($"{CinemaName} is now CLOSED.");
                projector.Stop();
            }
        }
        #endregion

        static void Main(string[] args)
        {
            #region Main Logic

            // Create cinema
            Cinema cinema = new Cinema("Cairo Cinema");

            // Open cinema
            cinema.OpenCinema();

            // Create tickets (hardcoded)
            StandardTicket t1 = new StandardTicket(
                "Avengers",
                new Seat('A', 5),
                80,
                "A5"
            );

            VIPTicket t2 = new VIPTicket(
                "Batman",
                new Seat('B', 2),
                150,
                true
            );

            IMAXTicket t3 = new IMAXTicket(
                "Interstellar",
                new Seat('C', 1),
                120,
                true
            );

            // Add tickets
            cinema.AddTicket(t1);
            cinema.AddTicket(t2);
            cinema.AddTicket(t3);

            // Print tickets
            cinema.PrintAllTickets();

            // Close cinema
            cinema.CloseCinema();

            #endregion
        }
    }
}