using CinemaSeatController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Kino_Pruefungsvorbereitung
{
    public class Program
    {

        private static Program instance;
        public static string csvUhrzeit;
        public static int csvSaal;
        public static string csvPlatz;
        public Program program;



        static void Main(string[] args)
        {
            SeatControllerUI seatControllerUI;
            //Deklaration und Initialisierung
            int anzahlSaele = 5;
            int eingabeSaal = 0;

            string vorstellung1 = "17:00";
            string vorstellung2 = "20:00";
            char switchcase;
            string eingabeUhrzeit = null;
           


            //Instanzbildung
            int[] sal1 = { 10, 15 };
            int[] sal2 = { 20, 10 };
            int[] sal3 = { 15, 20 };
            int[] sal4 = { 15, 20 };
            int[] sal5 = { 20, 20 };

            //Ändern der Farbe der Console auf:
            //Schwarzen Text und
            //einen weißen Hintergrund.

            
            //Eingabe "Auswahl"
            Console.WriteLine("Willkomen im 'Complex'!");
            Console.Write("Wollen Sie einen Reservierung vornehmen, dann drücken Sie 'R'. \nWollen Sie eine bereits vorgenommene Reservierung ansehen? Dann drücken Sie 'S'.");
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
                            Program.csvUhrzeit = (vorstellung1);
                        } else if (eingabeUhrzeit.Equals(vorstellung2))
                        {
                            Console.WriteLine("Sie haben erfolgreich die Vorstellung um {0} Uhr reserviert. ", vorstellung2);
                            Sitzplatz.normalPrice = 15;
                            Sitzplatz.premiumPrice = 12;
                            csvUhrzeit = vorstellung2;
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

                        kinoSaaleingabe:
                        //Hier beginnt die Eingabe zuy Asuswahl desKinosaals.
                        Console.Write("In welchen Kinosal wollen Sie gehen? ");
                        eingabeSaal = Convert.ToInt32(Console.ReadLine());

                        if(eingabeSaal>anzahlSaele)                                                         //Es wird überprüft, ob die Eingabe in dem möglichen Bereich liegt(kleiner/gleich 5 und größer/gleich 1)
                        {
                            Console.WriteLine("Fehler! Der Saal existiert nicht.");
                            goto kinoSaaleingabe;                                                           //Sollte die eingegebene Zahl nicht in dem Bereich liegen, so wird erneut nach einer Eingabe gefragt.
                        }else if (eingabeSaal < 1)                                                          //Es wird überprüft, ob die Eingabe in dem möglichen Bereich liegt(kleiner/gleich 5 und größer/gleich 1)
                        {
                            Console.WriteLine("Fehler! Der Saal existiert nicht!");                         //Ausgabe, dass der "angeforderte" Saal nicht existiert.
                            goto kinoSaaleingabe;                                                           //Sollte die eingegebene Zahl nicht in dem Bereich liegen, so wird erneut nach einer Eingabe gefragt.
                        }
                        else
                        {                                                                                   //Hier endet diese Eingabe. Je nach eingabe öffnet sich ein Fenster, zur Auswahl eines Sitzplatzes.
                            Console.WriteLine("Sie haben erfolgreich Sal {0} ausgewählt!",eingabeSaal);      //Ausgabe, dass die Eingabe erfolgreich war.
                            csvSaal = eingabeSaal;
                            Console.Clear();

                            switch (eingabeSaal)                                                            //Switch-Case zur Abfrage der Eingabe.
                            {
                                case 1:                                                                     //Sollte Saal 1 ausgewählt worden sein, wird
                                    seatControllerUI = new SeatControllerUI(sal1, eingabeUhrzeit);          //ein grafisches Fenster erstellt, in der Größe des oben festgelegten Arrays.
                                    
                                    break;
                                case 2:                                                                     //Sollte Saal 2 ausgewählt worden sein, wird
                                    seatControllerUI = new SeatControllerUI(sal2, eingabeUhrzeit);          //ein grafisches Fenster erstellt, in der Größe des oben festgelegten Arrays.
                                    
                                    break;
                                case 3:                                                                     //Sollte Saal 3 ausgewählt worden sein, wird
                                    seatControllerUI = new SeatControllerUI(sal3, eingabeUhrzeit);          //ein grafisches Fenster erstellt, in der Größe des oben festgelegten Arrays.
                                    
                                    break;
                                case 4:                                                                     //Sollte Saal 4 ausgewählt worden sein, wird
                                    seatControllerUI = new SeatControllerUI(sal4, eingabeUhrzeit);          //ein grafisches Fenster erstellt, in der Größe des oben festgelegten Arrays.
                                   
                                    break;
                                case 5:                                                                     //Sollte Saal 5 ausgewählt worden sein, wird
                                    seatControllerUI = new SeatControllerUI(sal5, eingabeUhrzeit);          //ein grafisches Fenster erstellt, in der Größe des oben festgelegten Arrays.
                                    
                                    break;
                                default:                                                                    //Dies wird ausgeführt, sollte die Ausgabe etwas anderem entsprechen, eigentlich irrelevant, allerdings scheint es nicht zu funktionieren wenn "default" weggelassen wird.
                                    seatControllerUI = new SeatControllerUI(sal1, "17:00");                 //ein grafisches Fenster erstellt, in der Größe des oben festgelegten Arrays.
                                    
                                    break;
                            }
                            Application.EnableVisualStyles();
                            Application.Run(seatControllerUI);
                            
                        }

                    }
                        
                    break;
                case 'S':
                    
                        Console.WriteLine("S");
                    break;

            }
            Console.ReadKey();

        }//Main
    }//class Program
}//namespace
