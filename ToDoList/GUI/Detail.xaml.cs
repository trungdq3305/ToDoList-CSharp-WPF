﻿using Repositories;
using Repositories.Entities;
using Services;
using System;
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

namespace GUI
{
    public partial class Detail : Window
    {
        private readonly NoteService _noteService = new(new NoteRepository(new ToDoListDbContext()));
        private Note _noteToUpdate;
        public Profile LoginedAccount { get; set; } = null;
        public Detail()
        {
            InitializeComponent();
        }
        public Detail(Note note) : this()
        {
            _noteToUpdate = note;

            TitleTextBox.Text = _noteToUpdate.Title;
            DescriptionTextBox.Text = _noteToUpdate.Description;
            ReminderDateTimePicker.Value = _noteToUpdate.Time;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_noteToUpdate != null)
            {
                _noteToUpdate.Title = TitleTextBox.Text;
                _noteToUpdate.Description = DescriptionTextBox.Text;
                _noteToUpdate.ModifiedDate = DateTime.Now;

                

                _noteService.UpdateNote(_noteToUpdate);
            }
            else 
            {
                Note newNote = new Note();
                //newNote.NoteId = GenerateNewId();
                
                newNote.Title = TitleTextBox.Text;
                newNote.ProfileId = 3;
                //newNote.ProfileId = LoginedAccount.ProfileId;
                newNote.Description = DescriptionTextBox.Text;
                newNote.ModifiedDate = DateTime.Now;
                newNote.Status = "Pending";
                newNote.Time = (DateTime)ReminderDateTimePicker.Value;




                _noteService.AddNote(newNote);
            }


            this.Close();


            var mainWindow = Application.Current.Windows.OfType<Home>().FirstOrDefault();
            if (mainWindow != null)
            {
                mainWindow.RefreshNotes(); 
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private  int currentId = 0;
        public int GenerateNewId()
        {
            int newId;
            List<Note> allNote = (List<Note>)_noteService.GetAllNotes();
            List<int> existedId = new List<int>();
            foreach (Note note in allNote)
            {
                existedId.Add(note.NoteId);
            }

            do
            {
                newId = ++currentId;
            } while (existedId.Contains(newId));

            existedId.Add(newId);

            return newId;
        }
    }
}
