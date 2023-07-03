using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SessionStatus.Domain
{
    public class Subject : INotifyPropertyChanged
    {
        private string _subjectStatus;
        public Subject(string subjectName, string subjectStatus)
        {
            this.SubjectName = subjectName;
            this._subjectStatus = subjectStatus;
        }

        public Subject()
        {
        }

        public string SubjectName { get; set; }
        public string SubjectStatus 
        { 
            get { return _subjectStatus; }
            set 
            {
                _subjectStatus = value;
                NotifyPropertyChanged();
            } 
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
