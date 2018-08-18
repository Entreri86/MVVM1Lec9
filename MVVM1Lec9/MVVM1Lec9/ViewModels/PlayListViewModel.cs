using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MVVM1Lec9.ViewModels
{
    public class PlayListViewModel : BaseViewModel
    {
        public string Title { get; set; }
        //Estos dos objetos Bool se utilizan para el DataBinding con la View, uno de ellos es accesible publicamente a la vista, el interno se comunica con
        //el modelo directamente siendo un campo de soporte (xRec), encapsulando los atributos por seguridad.
        private bool _isFavorite;

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                //SetValue(ref _isFavorite, value, nameof(_isFavorite));No funciona con el nameof en el 3 parametro
                SetValue(ref _isFavorite, value);

                OnPropertyChanged(nameof(Color));
            }
        }

        public Color Color
        {
            get { return IsFavorite ? Color.Pink : Color.Black; }
        }
    }
}
