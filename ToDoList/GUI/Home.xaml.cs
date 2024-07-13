﻿using Repositories.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GUI
{
    public partial class Home : Window
    {
        private readonly NoteService _noteService = new();

        public Home()
        {
            InitializeComponent();
        }


        private void AllBtn_Click(object sender, RoutedEventArgs e)
        {
            NotesDataGrid.ItemsSource = null;
            NotesDataGrid.ItemsSource = _noteService.GetAllNotes();
        }
        private void NotCompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            NotesDataGrid.ItemsSource = null;
            NotesDataGrid.ItemsSource = _noteService.GetNotCompleteNotes();
        }
        private void QuitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NotesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Detail detail = new Detail();
            detail.ShowDialog();
            NotesDataGrid.ItemsSource = null;
            NotesDataGrid.ItemsSource = _noteService.GetAllNotes();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var note = button.DataContext as Note;
            if (note != null)
            {
                Detail detail = new Detail(note);
                detail.ShowDialog();

                NotesDataGrid.ItemsSource = null;
                NotesDataGrid.ItemsSource = _noteService.GetAllNotes();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var note = button.DataContext as Note;

            if (note != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the note '{note.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _noteService.DeleteNoteById(note.NoteId);

                    RefreshNotes();
                }
            }
        }
        public void RefreshNotes()
        {
            NotesDataGrid.ItemsSource = null;
            NotesDataGrid.ItemsSource = _noteService.GetAllNotes();
        }

    }
    

}
