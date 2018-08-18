using MVVM1Lec9.Models;
using MVVM1Lec9.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MVVM1Lec9.ViewModels
{
    public class PlayListsViewModel : BaseViewModel
    {
        //Estos dos objetos PlayList se utilizan para el DataBinding con la View, uno de ellos es accesible publicamente a la vista, el interno se comunica con
        //el modelo directamente siendo un campo de soporte (xRec), encapsulando los atributos por seguridad.
        private PlayListViewModel _selectedPlayList;
        //Declaramos un atributo para recoger el valor del constructor.
        private readonly IPageService _pageService;
        //Declaramos las variable ICommand para eliminar los eventos de la Page derivando a esta clase.
        public ICommand AddPlayListCommand { get; private set; }
        public ICommand SelectPlayListCommand { get; private set; }
        //Todo en el ViewModel debe de ir Public para el DataBinding
        //La coleccion de canciones debe de ser recuperable desde afuera, pero tan solo se debe de poder asignar o crear desde esta clase misma.
        public ObservableCollection<PlayListViewModel> PlayLists { get; private set; } = new ObservableCollection<PlayListViewModel>();        
        public PlayListViewModel SelectedPlayList
        {
            get
            {
                return _selectedPlayList;//Si piden el valor no hay problema, se retorna.
            }
            set
            {
                //SetValue(ref _selectedPlayList, value, nameof(_selectedPlayList)); No funciona con el nameof en el 3 parametro
                SetValue(ref _selectedPlayList, value);
                /**
                if (_selectedPlayList == value) return;//Si el valor almacenado es el mismo que el de entrada del evento retornamos.

                _selectedPlayList = value;//De otra manera reasiganmos valores 

                OnPropertyChanged();//Y notificamos a la vista de ello deseleccionando el objeto de la lista.
                **/
            }
        }        
        //Declaramos un constructor que le pase por parametros el servicio de la pagina.
        public PlayListsViewModel(IPageService pageService)
        {
            this._pageService = pageService;
            //Iniciamos el comando del boton, pasandole el metodo como parametro de añadir la lista (debe de ser void al pasarlo como Action).
            AddPlayListCommand = new Command(AddPlayList);
            //Este segundo comando hay que pasarlo con una expresion Lambda, pues es la unica manera de inicializarlo al ser de tipo Task el retorno del metodo SelectPlayList y la clase
            //Command tan solo acepta de manera generica tipo Void. Por lo tanto utilizamos el metodo pasandole una accion lambda anonima como parametro.
            SelectPlayListCommand = new Command<PlayListViewModel>(async vm => await SelectPlayList(vm).ConfigureAwait(false));

        }
        
        private void AddPlayList()
        {
            var newPlayList = "PlayList" + (PlayLists.Count + 1);

            PlayLists.Add(new PlayListViewModel { Title = newPlayList });
        }

        private async Task SelectPlayList(PlayListViewModel playList)
        {
            if (playList == null) return;
            
            playList.IsFavorite = !playList.IsFavorite;//Cambiamos el estado del bool que estaba almacenado.
            SelectedPlayList = null;//Restauramos la vista a la normalidad quitando el foco.

            await _pageService.PushAsync(new PlayListDetailPage(playList));
        }

        
    }
}
