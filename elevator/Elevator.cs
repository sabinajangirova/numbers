using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevator
{
    class Elevator
    {
        private int NumberOfFloors { get; }
        private int LiftingCapacity { get; }

        private int CurrentFloor { get; set; }

        private int NumberOfPassengers { get; set; }

        public Elevator(int numberOfFloors, int liftingCapacity)
        {
            this.NumberOfFloors = numberOfFloors <= 0 ?  2 : numberOfFloors;
            this.LiftingCapacity = liftingCapacity <= 0 ? 1 : liftingCapacity;
            CurrentFloor = 1;
            NumberOfPassengers = 0;
        }

        public void LoadPassengers(int passengers)
        {
            Console.WriteLine("Door is open");
            if(passengers < 0)
            {
                Console.WriteLine("Invalid input: the number of loading passengers can't be negative");
                return;
            }
            if(LiftingCapacity < passengers + NumberOfPassengers)
            {
                NumberOfPassengers = LiftingCapacity;
                Console.WriteLine("The elevator is full. Can't take all people. The number of passengers is " + NumberOfPassengers);
            } else
            {
                NumberOfPassengers += passengers;
                Console.WriteLine("The number of passengers is " + NumberOfPassengers);
            }

            Console.WriteLine("Door is closed");
        }

        public void UnloadPassengers(int passengers)
        {
            Console.WriteLine("Door is open");
            if (passengers < 0)
            {
                Console.WriteLine("Invalid input: negative number of passengers");
                return;
            }


            if(NumberOfPassengers - passengers < 0)
            {
                NumberOfPassengers = 0;
                Console.WriteLine("Unloaded more passengers then there are in the elevator. The number of passengers now is "+ NumberOfPassengers);
            } else
            {
                NumberOfPassengers -= passengers;
                Console.WriteLine("The number of passengers is " + NumberOfPassengers);
            }
            Console.WriteLine("Door is closed");
        }

        public void GetElevator(int floorNumber, int passengers)
        {
            if(floorNumber < 1 || floorNumber > NumberOfFloors)
            {
                Console.WriteLine("The wanted floor is out of bounds");
                return;
            }
            GoTo(floorNumber);
            LoadPassengers(passengers);
        }

        public void Trip(int floorNumber, int passengers)
        {
            if (floorNumber < 1 || floorNumber > NumberOfFloors)
            {
                Console.WriteLine("The wanted floor is out of bounds");
                return;
            }
            GoTo(floorNumber);
            UnloadPassengers(passengers);
        }
        public void GoTo(int floorNumber)
        {
            if(floorNumber < 1 || floorNumber > NumberOfFloors)
            {
                Console.WriteLine("Invalid input: floorNumber is out of bounds");
                return;
            }
            
            if(floorNumber < CurrentFloor)
            {
                while(CurrentFloor > floorNumber)
                {
                    Console.WriteLine("Now on " + CurrentFloor +" floor");
                    CurrentFloor--;
                }

                Console.WriteLine("Now on " + CurrentFloor + " floor");
            } else
            {
                while (CurrentFloor < floorNumber)
                {
                    Console.WriteLine("Now on " + CurrentFloor + " floor");
                    CurrentFloor++;
                }

                Console.WriteLine("Now on " + CurrentFloor + " floor");
            }

        }
    }
}
