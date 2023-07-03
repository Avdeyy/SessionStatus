using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SessionStatus.Domain;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.IO;

namespace SessionStatus.Logic
{
    internal class Logical: INotifyPropertyChanged
    {
        public Logical()
        {
            StudySubjects.Add(new Subject("Физ-ра", status[0]));
            StudySubjects.Add(new Subject("Матан", status[1]));
            StudySubjects.Add(new Subject("Право", status[1]));
            StudySubjects.Add(new Subject("Дискретка", status[0]));

            FilterSubjects = StudySubjects;

            newSubject = new Subject();

            AddNewSubjectCommand = new RelayCommand(AddNewSubject);
            ChangeSubjectStatusCommand = new RelayCommand(ChangeStatus);
            DeleteSubjectCommand = new RelayCommand(DeleteSubject);

            PressAllCommand = new RelayCommand(PressAll);
            PressReadyCommand = new RelayCommand(PressReady);
            PressNoReadyCommand = new RelayCommand(PressNoReady);
            AcceptFilter = new RelayCommand(FSubjects);

            SaveFileCommand = new RelayCommand(SaveFile);
        }

        #region Properties
        string radioButtonName = "Все";

        public List<string> status { get; set; } = new List<string>()
        {
            "Зачет",
            "Незачет"
        };
        
        public ObservableCollection<Subject> StudySubjects { get; set; } = new ObservableCollection<Subject>();

        public ObservableCollection<Subject> FilterSubjects 
        { 
            get { return _filterSubjects; } 
            set
            {
                _filterSubjects = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Subject> _filterSubjects;

        private Subject _newSubject;

        public Subject newSubject
        {
            get { return _newSubject; }
            set
            {
                _newSubject = value;
                NotifyPropertyChanged();
            }
        }
        public Subject SelectedSubject { get; set; } = new Subject();
        #endregion

        #region WorkWithCollection
        public RelayCommand AddNewSubjectCommand { get; set; }
        public RelayCommand ChangeSubjectStatusCommand { get; set; }
        public RelayCommand DeleteSubjectCommand { get; set; }

        private void DeleteSubject()
        {
            for (int i = 0; i < StudySubjects.Count; i++)
            {
                if (StudySubjects[i].SubjectName.Equals(SelectedSubject.SubjectName)) 
                {
                    StudySubjects.Remove(StudySubjects[i]);
                    FSubjects();
                    SelectedSubject = new Subject();
                }
                
            }

        }
        private void ChangeStatus()
        {
            for (int i = 0; i < StudySubjects.Count; i++)
            {
                if (SelectedSubject != null)
                {
                    if (StudySubjects[i].SubjectName.Equals(SelectedSubject.SubjectName))
                    {
                        if (StudySubjects[i].SubjectStatus.Equals("Зачет")) { StudySubjects[i].SubjectStatus = "Незачет"; FSubjects(); }
                        else if (StudySubjects[i].SubjectStatus.Equals("Незачет")) { StudySubjects[i].SubjectStatus = "Зачет"; FSubjects(); }

                    }
                }
            }
        }
        private void AddNewSubject() 
        { 
            if (isCanAddSubject(newSubject.SubjectName) 
                && newSubject.SubjectName!= null 
                && newSubject.SubjectStatus != null)
            {
                StudySubjects.Add(new Subject(newSubject.SubjectName, newSubject.SubjectStatus));
                FSubjects();
                newSubject = new Subject();
            }
                
        }
        private bool isCanAddSubject(string name)
        {
            var selected = StudySubjects.Where(t => t.SubjectName.Equals(name)).ToList();
            if (selected.Count == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Такая дисциплина уже существует!");
                return false;

            }
        }
        #endregion

        #region Filter

        public RelayCommand PressAllCommand { get; set; }
        public RelayCommand PressReadyCommand { get; set; }
        public RelayCommand PressNoReadyCommand { get; set; }
        public RelayCommand AcceptFilter { get; set; }

        private void PressAll() => radioButtonName = "Все";

        private void PressReady() => radioButtonName = "Зачет";

        private void PressNoReady() => radioButtonName = "Незачет";

        private void FSubjects ()
        {
            if (radioButtonName.Equals("Все")) FilterSubjects = StudySubjects;
            if (radioButtonName.Equals("Зачет")) FilterSubjects = new ObservableCollection<Subject>(StudySubjects.Where(s => s.SubjectStatus.Equals("Зачет")).ToList());
            if (radioButtonName.Equals("Незачет")) FilterSubjects = new ObservableCollection<Subject>(StudySubjects.Where(s => s.SubjectStatus.Equals("Незачет")).ToList());

        }
        #endregion

        #region FileSave
        public RelayCommand SaveFileCommand { get; set; }

        private void SaveFile()
        {
            StreamWriter txt = new StreamWriter("D:\\StreamWriter.txt");
            for (int i = 0; i < StudySubjects.Count; i++)
            {
                txt.WriteLine($"{StudySubjects[i].SubjectName} - {StudySubjects[i].SubjectStatus}");
                txt.Flush();
            }
            MessageBox.Show("Файл успешно сохранен!");
        }

        #endregion

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
