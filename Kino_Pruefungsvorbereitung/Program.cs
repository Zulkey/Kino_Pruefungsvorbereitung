using CinemaSeatController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino_Pruefungsvorbereitung
{
    public class Program
    {

        private static Program instance;

        static void Main(string[] args)
        {
            //Deklaration und Initialisierung
            int anzahlSaele = 5;
            int eingabeSal = 0;

            string vorstellung1 = "17:00";
            string vorstellung2 = "20:00";
            char switchcase;
            // test
            string eingabeUhrzeit = null;


            //Instanzbildung
            int[] sal1 = { 10, 15 };
            int[] sal2 = { 20, 10 };
            int[] sal3 = { 15, 20 };
            int[] sal4 = { 15, 20 };
            int[] sal5 = { 20, 20 };




            //Eingabe "Auswahl"
            Console.WriteLine("Willkomen im 'Complex'!");
            Console.Write("Wollen Sie einen Reservierung vornehmen, dann drücken Sie 'R'. \nWollen Sie eine bereits vorgenommen Reservierung ansehen? Dann drücken Sie 'S'.");
            switchcase = Convert.ToChar(Console.ReadLine());

            Console.WriteLine();
            switchcase = Char.ToUpper(switchcase);
            switch (switchcase)
            {
                case 'R':
                    {
                        eingabeUhrzeitgoto:
                        Console.Write("Um wie viel Uhr wollen Sie den Film schauen?\n17:00 für 17 Uhr bzw. 20:00: ");
                        eingabeUhrzeit = Convert.ToString(Console.ReadLine());
                        Console.WriteLine();
                        
                        if (eingabeUhrzeit.Equals(vorstellung1))
                        {
                            Console.WriteLine("Sie haben erfolgreich die Vorstellung um {0} Uhr reserviert. ",vorstellung1);
                            Sitzplatz.normalPrice = 10;
                            Sitzplatz.premiumPrice = 8;
                        
                        } else if (eingabeUhrzeit.Equals(vorstellung2))
                        {
                            Console.WriteLine("Sie haben erfolgreich die Vorstellung um {0} Uhr reserviert. ", vorstellung2);
                            Sitzplatz.normalPrice = 15;
                            Sitzplatz.premiumPrice = 12;
                        }
                        else if(eingabeUhrzeit == null)
                        {
                            MessageBox.Show("Bitte gebe alle Daten korrekt ein!");
                            goto eingabeUhrzeitgoto;
                        } else
                        {
                            Console.WriteLine("Fehler, Uhrzeit nicht gefunden!");
                            goto eingabeUhrzeitgoto;
                        }
                        Console.WriteLine();

                        Console.Write("In welchen Kinosal wollen Sie gehen? ");
                        eingabeSal = Convert.ToInt32(Console.ReadLine());

                        if(eingabeSal>anzahlSaele)
                        {
                            Console.WriteLine("Fehler! Der Sal existiert nicht.");
                        }else
                        {
                            Console.WriteLine("Sie habe erfolgreich Sal {0} ausgewählt!",eingabeSal);
                        }

                    }
                        
                    break;
                case 'S':
                    
                        Console.WriteLine("S");
                    break;

            }




            SeatControllerUI seatControllerUI;
            switch (eingabeSal)
            {
                case 1:
                    seatControllerUI = new SeatControllerUI(sal1, eingabeUhrzeit);
                    break;
                case 2:
                    seatControllerUI = new SeatControllerUI(sal2, eingabeUhrzeit);
                    break;
                case 3:
                    seatControllerUI = new SeatControllerUI(sal3, eingabeUhrzeit);
                    break;
                case 4:
                    seatControllerUI = new SeatControllerUI(sal4, eingabeUhrzeit);
                    break;
                case 5:
                    seatControllerUI = new SeatControllerUI(sal5, eingabeUhrzeit);
                    break;
                default:
                    seatControllerUI = new SeatControllerUI(sal1, "17:00");
                    break;
            }
    

            Application.EnableVisualStyles();
            Application.Run(seatControllerUI);

            Console.ReadKey();

        }//Main
    }//class Program
}//namespace
