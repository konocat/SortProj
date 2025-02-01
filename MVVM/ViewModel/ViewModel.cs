using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SortProj.MVVM.Model;

namespace SortProj.MVVM.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly RandomizeModel _randomizeModel;
        private readonly SortingModel _sortingModel;
        private string _arraySize;
        private string _arrayOutput;
        private string _selectedMethod;
        private double _sortingTime;

        public ViewModel()
        {
            _randomizeModel = new RandomizeModel();
            _sortingModel = new SortingModel(); 
            GenerateCommand = new RelayCommand(param => GenerateArray());
            SortCommand = new RelayCommand(param => SortArray());
            ChangeMethodCommand = new RelayCommand(ChangeMethod);
        }

        public string ArraySize
        {
            get => _arraySize;
            set
            {
                _arraySize = value;
                OnPropertyChanged();
            }
        }
        public string ArrayOutput
        {
            get => _arrayOutput;
            set
            {
                _arrayOutput = value;
                OnPropertyChanged();
            }
        }
        public string SelectedMethod
        {
            get => _selectedMethod;
            set
            {
                if (_selectedMethod != value)
                {
                    _selectedMethod = value;
                    OnPropertyChanged();
                }
            }
        }
        public double SortingTime {
            get => _sortingTime;
            set {
                if (_sortingTime != value)
                {
                    _sortingTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GenerateCommand { get; }
        public ICommand SortCommand { get; }
        public ICommand ChangeMethodCommand { get; }

        private void ChangeMethod(object parameter)
        {
            if (parameter is string method)
            {
                SelectedMethod = method;
            }
        }
        private void SortArray()
        {
            if (!string.IsNullOrWhiteSpace(ArrayOutput))
            {
                try {
                    var stopwatch = Stopwatch.StartNew();
                    int[] nonSortedArray = ArrayOutput.Split(',').Select(int.Parse).ToArray();
                    string method = _selectedMethod;
                    int[] sortedArray = _sortingModel.SortArray(nonSortedArray, method);
                    ArrayOutput = string.Join(", ", sortedArray);
                    stopwatch.Stop();
                    SortingTime = stopwatch.ElapsedMilliseconds / 1000.0; // Время в секундах
                }
                catch { 
                }
                
            }
        }
        private void GenerateArray()
        {
            if (int.TryParse(ArraySize, out int size) && size > 0)
            {
                int[] array = _randomizeModel.GenerateRandomArray(size);
                ArrayOutput = string.Join(", ", array);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}