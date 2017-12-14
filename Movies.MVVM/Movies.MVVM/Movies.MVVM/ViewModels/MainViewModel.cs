using Movies.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.MVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private IReadOnlyList<Movie> _movies;
        public IReadOnlyList<Movie> Movies
        {
            get => _movies;
            private set
            {
                _movies = value;
                RaisePropertyChanged(nameof(Movies));
            }
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    RaisePropertyChanged();
                    OnSearchTermChangeAsync(_searchTerm)
                        .ContinueWith(tr => throw new Exception("Search Failed.", tr.Exception), 
                        TaskContinuationOptions.OnlyOnFaulted);
                }
            }
        }

        private CancellationTokenSource _cts;
        private async Task OnSearchTermChangeAsync(string searchTerm)
        {
            _cts?.Cancel();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var innerToken = _cts = new CancellationTokenSource();

                var movieService = new MovieService();
                var movies = await movieService.GetMoviesForSearchAsync(_searchTerm);
                if (!innerToken.IsCancellationRequested)
                {
                    Movies = movies;
                }
            }
        }
    }
}
