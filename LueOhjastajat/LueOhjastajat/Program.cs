//sovellus lukee tekstitiedostosta ohjastajatietoja ja näyttää ne konsolilla

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
        LueTiedostoV0("C:/temp/OhjastajienRavitilasto2016.txt");
      }
      catch (Exception ex)
      {
        Console.WriteLine("Ohjelman suorituksessa tapahtui virhe ", ex.Message);
      }
    }
    static void LueTiedostoV0(string tiedosto)
    {
      try
      {
        //luetaan tiedosto ja tallennetaan strukteihin
        // put each line of the text file into a string array. 
        string[] rivit = System.IO.File.ReadAllLines(tiedosto);
        //käydään läpi
        Ohjastaja kuski;
        int lkm = rivit.Length;
        Console.WriteLine(string.Format("Ohjastajia yhteensä {0}", lkm));
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
            //tulos ulos
            Console.WriteLine(string.Format("Ohjastaja {0} startit {1} voitot {2} voitto% {3}", kuski.Nimi, kuski.Startit, kuski.Voitot, kuski.VoittoPros));
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
    static void LueTiedostoV1(string tiedosto)
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
