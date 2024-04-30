using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using Component;
using Database;
using System.Threading;




namespace PROJECT_KE1
{
    class Program
    {
        public static AccessDatabaseHelper DB = new AccessDatabaseHelper("./Database1.accdb");
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            new Clear(1, 6, 30, 18).SetBackColor(ConsoleColor.DarkGray).Tampil();
            new Clear(1, 25, 119, 3).SetBackColor(ConsoleColor.DarkGray).Tampil();
            new Clear(31, 6, 88, 18).SetBackColor(ConsoleColor.DarkGray).Tampil();
            new Clear(1, 1, 119, 4).SetBackColor(ConsoleColor.DarkGray).Tampil();


            Kotak head = new Kotak();
            head.SetXY(0, 0).SetWidthAndHeight(119, 4).SetBackColor(ConsoleColor.Black).Tampil();
            new Kotak().SetXY(0, 25).SetWidthAndHeight(119, 3).SetBackColor(ConsoleColor.Black).Tampil();
            new Kotak().SetXY(0, 6).SetWidthAndHeight(30, 18).SetBackColor(ConsoleColor.Black).Tampil();
            new Kotak().SetXY(31, 6).SetWidthAndHeight(88, 18).SetBackColor(ConsoleColor.Black).Tampil();
          

            Tulisan sekolah = new Tulisan();
            sekolah.SetXY(1, 1).SetLength(119).Text = "APLIKASI BEL SEKOLAH";
            sekolah.SetBackColor(ConsoleColor.DarkGray).SetForeColor(ConsoleColor.Black);          
            sekolah.TampilTengah();



            Tulisan skul = new Tulisan();
            skul.SetXY(1, 2).SetLength(119).Text = "WEARNES EDUCATION CENTER MADIUN";
            skul.SetBackColor(ConsoleColor.DarkGray).SetForeColor(ConsoleColor.White);         
            skul.TampilTengah();



            Tulisan Alamat = new Tulisan();
            Alamat.SetXY(1, 3).SetLength(119).Text = "Jl. Thamrin 35A Kota Madiun";
            Alamat.SetBackColor(ConsoleColor.DarkGray).SetForeColor(ConsoleColor.Green);          
            Alamat.TampilTengah();


            Tulisan nama = new Tulisan();
            nama.SetText(" AMIRRUDIN RAHMAT UTAMA 7 ").SetXY(0, 26).SetLength(119);
            nama.SetBackColor(ConsoleColor.Magenta).SetForeColor(ConsoleColor.Cyan);           
            nama.TampilTengah();

            Tulisan kelas = new Tulisan();
            kelas.SetText("   INFORMATIKA II - ARCY  ").SetXY(0, 27).SetLength(119);
            kelas.SetBackColor(ConsoleColor.Magenta).SetForeColor(ConsoleColor.White);
            kelas.TampilTengah();



            Menu Menu = new Menu("JALANKAN", "LIHAT DATA", "TAMBAH DATA", "EDIT DATA", "HAPUS DATA", "KELUAR");
            Menu.SetXY(5, 10);
            Menu.ForeColor = ConsoleColor.Blue;
            Menu.SelectedBackColor = ConsoleColor.Gray;
            //Menu.SelectedBackColor = ConsoleColor.Gray;
            Menu.Tampil();

            Logo();
            bool IsProgramJalan = true;

            while (IsProgramJalan)
            {



                ConsoleKeyInfo Tombol = Console.ReadKey(true);
                if (Tombol.Key == ConsoleKey.DownArrow)
                {
                    Menu.Next();
                    Menu.Tampil();

                }
                else if (Tombol.Key == ConsoleKey.UpArrow)
                {
                    Menu.Prev();
                    Menu.Tampil();
                }

                else if (Tombol.Key == ConsoleKey.Enter)
                {
                    int MenuTerpilih = Menu.SelectedIndex;

                    if (MenuTerpilih == 0)
                    {
                        Jalankan();
                    }

                    else if (MenuTerpilih == 1)
                    {
                        LihatData();
                    }

                    else if (MenuTerpilih == 2)
                    {
                        TambahData();
                    }

                    else if (MenuTerpilih == 3)
                    {
                        EditData();
                    }

                    else if (MenuTerpilih == 4)
                    {
                        HapusData();
                    }

                    else if (MenuTerpilih == 5)
                    {
                        IsProgramJalan = false;
                    }



                }
            }

        }
        static void Jalankan()
        {
            new Clear(32, 7, 81, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText("-=JALANKAN PROGRAM=-").SetLength(89);
            Judul.TampilTengah();

            Tulisan HariSekarang = new Tulisan().SetXY(33, 9);
            Tulisan JamSekarang = new Tulisan().SetXY(33, 10);
            string QSelect = "SELECT * FROM tb_bel WHERE hari=@hari AND jam=@jam;";

            DB.Connect();

            bool Play = true;

            while (Play)
            {
                DateTime Sekarang = DateTime.Now;

                HariSekarang.SetText("HARI SEKARANG : " + Sekarang.ToString("dddd"));
                HariSekarang.Tampil();

                JamSekarang.SetText("JAM SEKARANG : " + Sekarang.ToString("HH:mm:ss"));
                JamSekarang.Tampil();

                DataTable DT = DB.RunQuery(QSelect,
                     new OleDbParameter("@hari", Sekarang.ToString("dddd")),
                     new OleDbParameter("@jam", Sekarang.ToString("HH:mm")));

                if (DT.Rows.Count > 0)
                {
                    Audio RAWR = new Audio();
                    RAWR.File = "./Moduleku/Suara/" + DT.Rows[0]["sound"];
                    RAWR.Play();

                    new Tulisan().SetXY(33, 14).SetText("BEL SUDAH BERBUNYI").SetBackColor(ConsoleColor.DarkGray).SetForeColor(ConsoleColor.DarkBlue).SetLength(88).TampilTengah();
                    new Tulisan().SetXY(33, 15).SetText(DT.Rows[0]["ket"].ToString()).SetBackColor(ConsoleColor.DarkGray).SetForeColor(ConsoleColor.DarkBlue).SetLength(88).TampilTengah();
                    Play = false;
                }
                Thread.Sleep(1000);
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo Tombol = Console.ReadKey(true);
                    if (Tombol.Key == ConsoleKey.Delete)
                    {
                        Play = false;
                        new Clear(32, 7, 88, 16).Tampil();
                        Logo();
                    }
                }
            }
        }
        static void TambahData()
        {
            new Clear(32, 7, 81, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText("-=TAMBAH DATA =-").SetLength(89);
            Judul.TampilTengah();

            Inputan HARIinput = new Inputan();
            HARIinput.Text = "Masukkan Hari :";
            HARIinput.SetXY(33, 9);


            Inputan JAMinput = new Inputan();
            JAMinput.Text = "Masukkan Jam :";
            JAMinput.SetXY(33, 10);


            Inputan KETinput = new Inputan();
            KETinput.Text = "Masukkan Keterangan :";
            KETinput.SetXY(33, 11);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(

                "5 Menit Akhir Istirahat I.wav",
                 "5 Menit Akhir Istirahat II.wav",
                  "5 Menit Akhir Kegiatan Keagamaan.wav",
               "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara.wav",
                 "Akhir Pekan 1.wav",
                 "Akhir Pekan 2.wav",
                 "Akhir Pelajaran A.wav",
                   "Akhir Pelajaran B.wav",
                     "awal jam ke1.wav",
                       "Istirahat I.wav",
                      "Istirahat II.wav",
                    " Pelajaran ke 1.wav",
                     " Pelajaran ke 2.wav",
                      " Pelajaran ke 3.wav",
                       " Pelajaran ke 4.wav",
                        " Pelajaran ke 5.wav",
                         " Pelajaran ke 6.wav",
                          " Pelajaran ke 7.wav",
                           " Pelajaran ke 8.wav",
                      " Pelajaran ke 9.wav",
                      "pembuka.wav");

            SoundInput.Text = "Masukan Audio :";
            SoundInput.SetXY(33, 12);

            string HARI = HARIinput.Read();
            string JAM = JAMinput.Read();
            string KETERANGAN = KETinput.Read();
            string SOUND = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("INSERT INTO tb_bel (hari,jam,ket, sound) VALUES (@hari,@jam,@ket,@sound);",
               new OleDbParameter("@hari", HARI),
                new OleDbParameter("@jam", JAM),
                 new OleDbParameter("@ket", KETERANGAN),
                  new OleDbParameter("@sound ", SOUND)
                  );
            DB.Disconnect();


            new Tulisan().SetXY(31, 14).SetText("DATA BERHASIL DI SIMPAN!!!").SetBackColor(ConsoleColor.DarkGray).SetForeColor(ConsoleColor.DarkBlue).SetLength(88).TampilTengah();


        }
        static void LihatData()
        {
            new Clear(32, 7, 81, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText("-=LIHAT DATA JADWAL=-").SetLength(89);
            Judul.TampilTengah();

            DB.Connect();
            DataTable DT = DB.RunQuery("SELECT * FROM tb_bel;");
            DB.Disconnect();

            new Tulisan("┌──────┬────────────────┬──────────┬───────────────────────────────────────────────┐").SetXY(34, 10).Tampil();
            new Tulisan("│  NO  │     HARI       │  JAM     │            KETERANGAN                         │").SetXY(34, 11).Tampil();
            new Tulisan("├──────┼────────────────┼──────────┼───────────────────────────────────────────────┤").SetXY(34, 12).Tampil();
            for (int i = 0; i < DT.Rows.Count; i++)
            {

                string ID = DT.Rows[i]["id"].ToString();
                string HARI = DT.Rows[i]["hari"].ToString();
                string JAM = DT.Rows[i]["jam"].ToString();
                string KETERANGAN = DT.Rows[i]["ket"].ToString();

                string isi = string.Format("│{0,-6}│{1,-16}│{2,-10}│{3,-47}│", ID, HARI, JAM, KETERANGAN);
                new Tulisan(isi).SetXY(34, 13 + i).Tampil();

            }


            new Tulisan("└──────┴────────────────┴──────────┴───────────────────────────────────────────────┘").SetXY(34, 13 + DT.Rows.Count).Tampil();


        }
        static void EditData()
        {
            new Clear(32, 7, 81, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText("-=EDIT DATA=-").SetLength(89);
            Judul.TampilTengah();

            Inputan IDinputrubah = new Inputan();
            IDinputrubah.Text = "Masukan ID Jadwal Yang Ingin Di Rubah :";
            IDinputrubah.SetXY(33, 9);


            Inputan HARIinput = new Inputan();
            HARIinput.Text = "Masukkan Hari :";
            HARIinput.SetXY(33, 10);


            Inputan JAMinput = new Inputan();
            JAMinput.Text = "Masukkan Jam :";
            JAMinput.SetXY(33, 11);


            Inputan KETinput = new Inputan();
            KETinput.Text = "Masukkan Keterangan :";
            KETinput.SetXY(33, 12);




            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(

                "5 Menit Akhir Istirahat I.wav",
                 "5 Menit Akhir Istirahat II.wav",
                  "5 Menit Akhir Kegiatan Keagamaan.wav",
               "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara.wav",
                 "Akhir Pekan 1.wav",
                 "Akhir Pekan 2.wav",
                 "Akhir Pelajaran A.wav",
                   "Akhir Pelajaran B.wav",
                     "awal jam ke1.wav",
                       "Istirahat I.wav",
                      "Istirahat II.wav",
                    " Pelajaran ke 1.wav",
                     " Pelajaran ke 2.wav",
                      " Pelajaran ke 3.wav",
                       " Pelajaran ke 4.wav",
                        " Pelajaran ke 5.wav",
                         " Pelajaran ke 6.wav",
                          " Pelajaran ke 7.wav",
                           " Pelajaran ke 8.wav",
                      " Pelajaran ke 9.wav",
                      "pembuka.wav");
            SoundInput.Text = "Masukan Audio :";
            SoundInput.SetXY(33, 14);

            string IDRubah = IDinputrubah.Read();
            string HARI = HARIinput.Read();
            string JAM = JAMinput.Read();
            string KETERANGAN = KETinput.Read();
            string SOUND = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("UPDATE tb_bel SET hari=@hari,jam=@jam,ket=@ket,sound=@sound WHERE id=@id;",
               new OleDbParameter("@hari", HARI),
                new OleDbParameter("@jam", JAM),
                 new OleDbParameter("@ket", KETERANGAN),
                  new OleDbParameter("@sound ", SOUND),
                   new OleDbParameter("@id ", IDRubah)
                  );
            DB.Disconnect();


            new Tulisan().SetXY(31, 16).SetText("DATA BERHASIL DI EDIT  !!!").SetBackColor(ConsoleColor.DarkGray).SetForeColor(ConsoleColor.DarkBlue).SetLength(88).TampilTengah();



        }
        static void HapusData()
        {
            new Clear(32, 7, 81, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText("-=HAPUS DATA=-").SetLength(89);
            Judul.TampilTengah();

            Inputan IDinput = new Inputan();
            IDinput.Text = "MASUKAN ID YANG AKAN DI HAPUS :";
            IDinput.SetXY(34, 9);
            string ID = IDinput.Read();

            DB.Connect();
            DB.RunNonQuery("DELETE FROM tb_bel WHERE id=@id;",
            new OleDbParameter("@id", ID));
            DB.Disconnect();
            new Tulisan().SetXY(31, 12).SetText("DATA BERHASIL DI HAPUS !!!").SetBackColor(ConsoleColor.Gray).SetForeColor(ConsoleColor.DarkBlue).SetLength(88).TampilTengah();

        }
        static void Logo()
        {



            new Kotak().SetXY(58, 7).SetWidthAndHeight(32, 6).SetBackColor(ConsoleColor.Magenta).Tampil();
            new Kotak().SetXY(57, 7).SetWidthAndHeight(34, 7).SetBackColor(ConsoleColor.DarkBlue).Tampil();
            new Kotak().SetXY(52, 10).SetWidthAndHeight(45, 10).SetBackColor(ConsoleColor.Magenta).Tampil();
            new Kotak().SetXY(49, 13).SetWidthAndHeight(51, 10).SetBackColor(ConsoleColor.DarkBlue).Tampil();

            new Tulisan("██╗░░██╗░█████╗░██╗██╗██╗██").SetXY(31, 7).SetLength(88).SetForeColor(ConsoleColor.Cyan).TampilTengah();
            new Tulisan("██║░░██║██╔══██╗██║██║██║██").SetXY(31, 8).SetLength(88).SetForeColor(ConsoleColor.Cyan).TampilTengah();
            new Tulisan("███████║███████║██║██║██║██║").SetXY(31, 9).SetLength(88).SetForeColor(ConsoleColor.Cyan).TampilTengah();
            new Tulisan("██║░░██║██║░░██║██║██║██║██║").SetXY(31, 10).SetLength(88).SetForeColor(ConsoleColor.Cyan).TampilTengah();
            new Tulisan("╚═╝░░╚═╝╚═╝░░╚═╝╚═╝╚═╝╚═╝╚═╝").SetXY(31, 11).SetLength(88).SetForeColor(ConsoleColor.Cyan).TampilTengah();
            new Tulisan("     .-+*++++=::          :-+*##*+=:      ").SetXY(31, 14).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan("   .*%%%#****#%%%*:    :+%@@%###%%%#*=.   ").SetXY(31, 15).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan("  :**#+==+**+==+#%@#-:*@@%*===++==+*%%*-  ").SetXY(31, 16).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan(" :**#=+##*#%@%%*=+*%@@@#++#%%@@@@@#+=%#*: ").SetXY(31, 17).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan(" ++#+++=+:..:+%@@#+++*+*###*=:..:###++%#+ ").SetXY(31, 18).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan(" **#*@#==      +@@%=  ==##=      +%#++%** ").SetXY(31, 18).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan(" ++%*@%-:   .=+**##%%%%%%*=:    .++#++%#+ ").SetXY(31, 19).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan(" :**##%%*: =#*****%*-:+%@@%#+- -***++%**: ").SetXY(31, 20).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan("  :+*#+===---=+##+:   -:=#@@%#**-:=*=-*:  ").SetXY(31, 21).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();
            new Tulisan("   :=**+++::. :       ::=++**##+++=+-.    ").SetXY(31, 22).SetLength(88).SetBackColor(ConsoleColor.White).SetForeColor(ConsoleColor.Black).TampilTengah();

























        }
    }
}



