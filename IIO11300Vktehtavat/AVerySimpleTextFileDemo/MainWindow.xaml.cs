/*
* Copyright JAMK/IT/ETS
* This file is part of the course IIO11300 Windows-ohjelmointi
* Created: 28.1.2016 
* Authors:Esa Salmikangas 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AVerySimpleTextFileDemo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnWrite_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        //kirjoitetaan annettu teksti tiedostoon riittävän monta kertaa
        string filename = txtFileName.Text;
        int count = 1;
        int.TryParse(txtCount.Text, out count);
        if (filename.Length > 0)
        {
          using (StreamWriter sw = File.CreateText(filename))
          {
            for (int i = 1; i <= count; i++)
            {
              sw.WriteLine(i.ToString() + ":" + txtText.Text);
            }
          }
          tbMessages.Text = String.Format("Kirjoitettu {0} riviä tiedostoon {1}", count, filename);
        }
      }
      catch (Exception ex)
      {

        tbMessages.Text = ex.Message;
      }
    }

    private void btnReadFile_Click(object sender, RoutedEventArgs e)
    {
      txtResult.Text = null;
      //luetaan teksitiedostoa rivi kerrallaan
      string filename = txtFileName.Text;
      string line = null;
      if (filename.Length > 0)
      {
        using (StreamReader sr = File.OpenText(filename))
        {
          line = null;
          do
          {
            line = sr.ReadLine();
            txtResult.Text += line + "\n";
          } while (line != null);
        }
      }
    }
  }
}
