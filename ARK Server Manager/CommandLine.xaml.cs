﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ARK_Server_Manager.Lib;

namespace ARK_Server_Manager
{
    /// <summary>
    /// Interaction logic for CommandLine.xaml
    /// </summary>
    public partial class CommandLine : Window
    {
        public CommandLine(string commandLine)
        {
            InitializeComponent();
            WindowUtils.RemoveDefaultResourceDictionary(this);

            this.DataContext = commandLine;
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Clipboard.SetText(this.DataContext as string);
                MessageBox.Show("Done!", "Copied to clipboard", MessageBoxButton.OK);
            }
            catch
            {
                MessageBox.Show("Clipboard could not be opened.  Another application may be using it.  Please try closing other applications and trying again.", "Copy to clipboard failed.", MessageBoxButton.OK);
            }            
        }
    }
}
