using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using UniversalUseStorage.Annotations;

namespace UniversalUseStorage
{
    class ViewModel : INotifyPropertyChanged
    {
        public RelayCommand ButtonLoad { get; set; }

        public RelayCommand ButtonSave { get; set; }

        public TextFileModel Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private TextFileModel _text;
        public ViewModel()
        {
            Text = new TextFileModel();
            ButtonLoad = new RelayCommand(loadText);
            ButtonSave = new RelayCommand(saveTextAsync);
        }

        public async void saveTextAsync()
        {
            Debug.WriteLine("Saving text async...");
            await XmlReadWriteUniversal.SaveObjectToXml<TextFileModel>(Text, "TextFileModel.xml");
            Debug.WriteLine("Text: " + Text.Text);
        }

        private async void loadText()
        {
            Debug.WriteLine("loading text async...");
            Text = await XmlReadWriteUniversal.ReadObjectFromXmlFileAsync<TextFileModel>("TextFileModel.xml");
            Debug.WriteLine("text:" + Text.Text);
            OnPropertyChanged("Text");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
