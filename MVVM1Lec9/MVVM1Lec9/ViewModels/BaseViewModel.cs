using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MVVM1Lec9.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Propiedad protected para que solo lo puedan utilizar derivados de esta misma clase
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Metodo generico de clase para poder reutilizarlo en todas las clases que lo implementen y asi no tener codigo repetido en todas las clases.
        //El campo de soporte tiene la palabra reservada "ref" para pasar el valor por referencia hacia el objeto real. Se añade el parametro propertyName
        //para pasarle el nombre de la propiedad desde el objeto derivado a este.
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            //Metodo de la clase EqualityComparer que compara dos objetos genericos en tiempo de ejecución y retorna bool true si son iguales, en caso de no serlo
            //se asigna el valor y se notifica a la vista.
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;
            
            backingField = value;

            OnPropertyChanged(propertyName);
        }
    }
}
