//Esa Salmikangas 11.-12.1.2017
//sovellus lukee tekstitiedostosta ohjastajatietoja ja näyttää ne konsolilla
//tekstitiedosto haettu heppa.hippos.fi 11.1.2017
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT
{
  struct Ohjastaja
  {
    //perusteellinen selvitys struct-rakenteesta
    //https://www.dotnetperls.com/struct
    public string Nimi;
    public int Startit;
    public int Voitot;
    public float VoittoPros;
  }
  class LueOhjastajat
  {
    static void Main(string[] args)
    {
      try
      {
        LueTiedostoVerSimple("C:/temp/OhjastajienRavitilasto2016.txt");
        //LueTiedostoVerStruct("C:/temp/OhjastajienRavitilasto2016.txt");
      }
      catch (Exception ex)
      {
        Console.WriteLine("Ohjelman suorituksessa tapahtui virhe ", ex.Message);
      }
    }
    static void LueTiedostoVerSimple(string tiedosto)
    {
      //luetetaan tiedosto läpi rivi kerrallaan
      //https://msdn.microsoft.com/en-us/library/aa287535(v=vs.71).aspx
      try
      {
        //luetaan tiedosto ja tallennetaan strukteihin
        //each line of the text file into a string array. 
        int counter = 0;
        string line;
        // Read the file and display it line by line.
        System.IO.StreamReader file =
           new System.IO.StreamReader(tiedosto);
        while ((line = file.ReadLine()) != null)
        {
          Console.WriteLine(counter  + ":" + line);
          counter++;
        }
        file.Close();
        // Suspend the screen.
        Console.ReadLine();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    static void LueTiedostoVerStruct(string tiedosto)
    {
      try
      {
        //luetaan tiedosto ja tallennetaan strukteihin
        //each line of the text file into a string array. 
        string[] rivit = System.IO.File.ReadAllLines(tiedosto);
        //käydään läpi
        Ohjastaja kuski;
        int lkm = rivit.Length;
        Console.WriteLine(string.Format("Ohjastajia yhteensä {0}", lkm));
        //from string array to struct
        for (int i = 3; i < lkm; i++)
        {
          string[] sanat = rivit[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
          //huomioidaan otsikko rivit
          if (sanat.Length > 2)
          {
            kuski.Nimi = sanat[0];
            kuski.Startit = int.Parse(sanat[1]);
            kuski.Voitot = int.Parse(sanat[2]);
            kuski.VoittoPros = (100F * kuski.Voitot / kuski.Startit);
            //tulos ulos, pysäytetään 500 ja 1000 kohdalla
            if (i == 500 || i == 1000)
            { Console.WriteLine("Press any key to continue"); Console.ReadKey(); }
            Console.WriteLine(string.Format("{4}. ohjastaja {0} startit {1} voitot {2} voitto% {3}", kuski.Nimi, kuski.Startit, kuski.Voitot, kuski.VoittoPros, i));
          }
        }
        //lopetus
        Console.WriteLine("That' all folks!");
        Console.ReadLine();
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }
    //luetaan tiedosto DataTableen DataRow-olioina
    static void LueTiedostoVerDT(string tiedosto)
    {
      try
      {
        //luetaan tiedosto muistiin
        DataTable dt = new DataTable();
        // put each line of the text file into a string array. 
        string[] lines = System.IO.File.ReadAllLines(tiedosto);

        // loop thru each line
        for (int i = 2; i < lines.Length; i++)
        {
          DataRow newrow = dt.NewRow();

          // split each line into words by spaces
          string[] words = lines[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

          // if i = 0, means this is the header row
          if (i == 2)
          {
            for (int j = 0; j < words.Length; j++)
            {
              // add column
              dt.Columns.Add(words[j]);
            }
          }
          else
          {
            for (int j = 0; j < words.Length; j++)
            {
              // fill cell content
              newrow[dt.Columns[j]] = words[j];
            }

            // add new row
            dt.Rows.Add(newrow);
          }
        }
        //output
        Console.WriteLine("Ohjastajat");
        foreach (DataRow dr in dt.Rows)
        {
          for (int i = 0; i < 11; i++)
          {
            Console.Write(dr[i]);
          }
          Console.Write("\n");
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
