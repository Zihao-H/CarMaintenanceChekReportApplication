using System;
using System.Collections.Generic;
using System.IO;

namespace CarMaintenanceChekReportApplication
{
    public class Technician
    {
        public int TechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianSkill { get; set; }
    }
    public class TechnicianAllocation
    {
        public int AllocationTechnicianId { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianSkill { get; set; }
        public Car AllocatedCar { get; set; }
    }
    public class Car
    {
        public string CarOwner { get; set; }
        public string CarRogoA { get; set; }
        public int CarRogoNum { get; set; }
    }

    internal class Program
    {
        public static Technician[] listoftechnicians;
        public static Car[] listofcar;

        public static List<TechnicianAllocation> listOfTechnicianAllocaltions = new List<TechnicianAllocation>();
        static void AddTechnician()
        {
            try
            {
                Console.WriteLine("Please Enter How many Technician you need:");
                int numtechnician = Convert.ToInt32(Console.ReadLine());
                listoftechnicians = new Technician[numtechnician];
                for (int i = 0; i < numtechnician; i++)
                {
                    Technician Technician = new Technician();
                    Console.WriteLine("Please Enter Technician ID number :");
                    Technician.TechnicianId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Please Enter Technician Name:");
                    Technician.TechnicianName = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Please Enter Technician Skll:");
                    Technician.TechnicianSkill = Convert.ToString(Console.ReadLine());
                    listoftechnicians[i] = Technician;
                }
            }
            catch { Console.WriteLine("Please try again"); }
        }
        static void DisplayTechnicians()
        {
            foreach (Technician Technician in listoftechnicians)
            {
                Console.WriteLine("***********************************************\n");
                Console.WriteLine(Technician.TechnicianId);
                Console.WriteLine(Technician.TechnicianName);
                Console.WriteLine(Technician.TechnicianSkill + "\n");
                Console.WriteLine("***********************************************\n");
            }
        }

        static void AllocateCARforCheck()
        {
            Console.WriteLine("Allocate Technician for your Car");
            {
                try
                {
                    Console.WriteLine("Please Enter How many car you:");
                    int numCar = Convert.ToInt32(Console.ReadLine());
                    listofcar = new Car[numCar];
                    for (int n = 0; n < numCar; n++)
                    {
                        Car car = new Car();
                        Console.WriteLine("Please Enter Name of Car Owner  :");
                        car.CarOwner = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Please Enter Rogo Alphabet:");
                        car.CarRogoA = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Please Enter Rogo Number");
                        car.CarRogoNum = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please Enter the Technician ID:");
                        int TechnicianId = Convert.ToInt32(Console.ReadLine());
                        listofcar[n] = car;

                        bool technicianFound = false;

                        for (int i = 0; i < listoftechnicians.Length; i++)
                        {
                            if (TechnicianId == listoftechnicians[i].TechnicianId)
                            {
                                technicianFound = true;
                                break;
                            }
                        }
                        if (technicianFound)
                        {
                            TechnicianAllocation technicianAllocation = new TechnicianAllocation();
                            technicianAllocation.AllocationTechnicianId = TechnicianId;
                            technicianAllocation.AllocatedCar = car;

                            listOfTechnicianAllocaltions.Add(technicianAllocation);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Technician ID....");
                        }


                    }
                }
                catch { Console.WriteLine("Please try again"); }
            }
        }
        static void DeallocateCarafterCheck()
        {
            Console.WriteLine("DeAllocate Technician function is called from Main function switch");
            Console.WriteLine("Please Enter the Technician ID");
            int TechnicianId = Convert.ToInt32(Console.ReadLine());

            bool technicianFound = false;
            for (int i = 0; i < listoftechnicians.Length; i++)
            {
                if (TechnicianId == listoftechnicians[i].TechnicianId)
                {
                    technicianFound = true;
                    break;
                }
            }
            if (technicianFound)
            {
                TechnicianAllocation technicianAllocation = listOfTechnicianAllocaltions.Find(x => x.AllocationTechnicianId == TechnicianId);
                listOfTechnicianAllocaltions.Remove(technicianAllocation);
            }
            else
            {
                Console.WriteLine("Wrong Room Number...");
            }
        }

        static void DisplayCARAllocationDetails()
        {
            Console.WriteLine("ShowCurrentStatus() function is called from Main function switch");
            Console.WriteLine("**************************** Technicians Allocations Details ****************************************");
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine("Technician ID\t Car RegoNo\t Customer Name");
            Console.WriteLine("------------------------------------------------------------------------------------");


            foreach (TechnicianAllocation technicianAllocation in listOfTechnicianAllocaltions)
            {
                Console.WriteLine(technicianAllocation.AllocationTechnicianId + "\t\t" + technicianAllocation.AllocatedCar.CarRogoA + technicianAllocation.AllocatedCar.CarRogoNum + "\t\t" + technicianAllocation.AllocatedCar.CarOwner);
            }
        }
        static void Billing()
        {
            Console.WriteLine("Billing function is called from Main function switch");
        }
        public static string filePath;
        public static string filePatha;

        static void SavetheCARAllocationsDetailsToaFile()
        {
            Console.WriteLine("Save the CAR Allocations Details To a File function is called from Main function switch");
            FileStream f = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter streamWriter = new StreamWriter(f);

            DateTime now = DateTime.Now;
            foreach (var technicianAllocation in listOfTechnicianAllocaltions)
            {
                string strToAdd = "TechnicianID: " + technicianAllocation.AllocationTechnicianId + ", RegistrationNo: " + technicianAllocation.AllocatedCar.CarRogoA + technicianAllocation.AllocatedCar.CarRogoNum + ", CustomerName: " + technicianAllocation.AllocatedCar.CarOwner + now;
                streamWriter.WriteLine(strToAdd);
            }
            streamWriter.Close();
        }

        static void ShowtheCARAllocationsDetailsFromaFile()
        {
            FileStream f = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);

            StreamReader streamReader = new StreamReader(f);

            //StreamReader streamReader = new StreamReader(filePath);

            string line = streamReader.ReadLine();

            while (line != null)

            {
                Console.WriteLine(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();
        }
        static void CopyTheFile()
        {
            File.Copy(filePath, filePatha);
            File.Delete(filePath);
        }

        static void Main(string[] args)
        {
            string folderpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderpath, "CarCarCar.txt");

            string folderpatha = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePatha = Path.Combine(folderpatha, "Backup.txt");
            char ans;
            try
            {
                do
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("***********************************************************************************");
                        Console.WriteLine("                 VTNZ Maintenance Check Report                  ");
                        Console.WriteLine("                            MENU                                 ");
                        Console.WriteLine("***********************************************************************************");
                        Console.WriteLine("1. Add Technician");
                        Console.WriteLine("2. Display Technicians");
                        Console.WriteLine("3. Allocate CAR for Check");
                        Console.WriteLine("4. De-Allocate Car after Check");
                        Console.WriteLine("5. Display CAR Allocation Details");
                        Console.WriteLine("6. Billing");
                        Console.WriteLine("7. Save the CAR Allocations Details To a File");
                        Console.WriteLine("8. Show the CAR Allocations Details From a File");
                        Console.WriteLine("9. Exit");
                        Console.WriteLine("10. Copy the File ");
                        Console.WriteLine("***********************************************************************************");
                        Console.Write("Enter Your Choice Number Here:");
                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                Console.Clear();
                                AddTechnician();


                                break;
                            case 2:
                                Console.Clear();
                                DisplayTechnicians();


                                break;
                            case 3:
                                Console.Clear();
                                AllocateCARforCheck();


                                break;
                            case 4:
                                Console.Clear();
                                DeallocateCarafterCheck();


                                break;
                            case 5:
                                Console.Clear();
                                DisplayCARAllocationDetails();


                                break;
                            case 6:
                                Console.Clear();
                                Billing();


                                break;
                            case 7:
                                Console.Clear();
                                SavetheCARAllocationsDetailsToaFile();


                                break;
                            case 8:
                                Console.Clear();
                                ShowtheCARAllocationsDetailsFromaFile();


                                break;
                            case 9:
                                Environment.Exit(0);
                                break;
                            case 10:
                                Console.Clear();
                                CopyTheFile();
                                break;
                            default:
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Please try again");
                    }
                    Console.Write("\nWould You Like To Continue(Y/N):");
                    ans = Convert.ToChar(Console.ReadLine());
                } while (ans == 'y' || ans == 'Y');
            }catch 
            { Console.WriteLine("Please try again");
            }
        }
    }
}


