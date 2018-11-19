using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;
using System.IO.MemoryMappedFiles;
using Lab4.Commands;
using Lab4.Models;

namespace Lab4.ViewModels
{
    class MainViewModel:INotifyPropertyChanged
    {
        private string selectedTransform;
#if DEBUG
        private string outputInfo = "0,0";
#endif
        private ObservableCollection<string> listTransform = new ObservableCollection<string>();
        private WriteableBitmap wb;
        private const byte BRIGHTNESS_CUT_START = 100;
        private const byte BRIGHTNESS_CUT_END = 200;
        private string imagePath = "pack://application:,,/Resources/скачанные файлы.jpg";
        private int ImageCount = 0;

        public string SelectedTransform
        {
            get { return selectedTransform; }
            set
            {
                selectedTransform = value;
                OnPropertyChanged();
            }
        }
        public WriteableBitmap Wb
        {
            get
            {
                return wb;
            }

            set
            {
                wb = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> ListTransform
        {
            get { return listTransform; }
            set
            {
                listTransform = value;
                OnPropertyChanged();
            }
        }
#if DEBUG
        public string OutputInfo
        {
            get
            {
                return outputInfo;
            }

            set
            {
                outputInfo = value;
                OnPropertyChanged();
            }
        }
#endif
        public MainViewModel()
        {
            //Image.Source = new BitmapImage(new Uri("pack://application:,,/Resources/Shining.jpg"));
            //wb = new WriteableBitmap(new BitmapImage(new Uri(imagePath)));//BitmapFactory.FromContent("/Resources/скачанные файлы.jpg");

            listTransform.Add("LinearContrast");
            listTransform.Add("ThresholdProcessing");
            listTransform.Add("BrightnessСut_A");
            listTransform.Add("BrightnessСut_B");
            listTransform.Add("BrightnessСut_C");
            listTransform.Add("ContrastScaling_A");
            listTransform.Add("ContrastScaling_B");
            listTransform.Add("Negative");
            listTransform.Add("ContourSelection");
            listTransform.Add("RandomPoints");
            listTransform.Add("MedianFiltering");
            listTransform.Add("ToBlack");
            SelectedTransform = listTransform[0];
        }

        #region Commands       
        public DelegateCommand Apply
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    switch (SelectedTransform)
                    {
                        case "LinearContrast":
                            {
                                MainModel.LinearContrast(wb, 0, 100);
                                break;
                            }
                        case "ThresholdProcessing":
                            {
                                MainModel.ThresholdProcessing(wb, BRIGHTNESS_CUT_START);
                                break;
                            }
                        case "BrightnessСut_A":
                            {
                                MainModel.BrightnessСut_A(wb, BRIGHTNESS_CUT_START, BRIGHTNESS_CUT_END);
                                break;
                            }
                        case "BrightnessСut_B":
                            {
                                MainModel.BrightnessСut_B(wb, BRIGHTNESS_CUT_START, BRIGHTNESS_CUT_END);
                                break;
                            }
                        case "BrightnessСut_C":
                            {
                                MainModel.BrightnessСut_C(wb, BRIGHTNESS_CUT_START);
                                break;
                            }
                        case "ContrastScaling_A":
                            {
                                MainModel.ContrastScaling_A(wb, BRIGHTNESS_CUT_START, BRIGHTNESS_CUT_END);
                                break;
                            }
                        case "ContrastScaling_B":
                            {
                                MainModel.ContrastScaling_B(wb, BRIGHTNESS_CUT_START, BRIGHTNESS_CUT_END);
                                break;
                            }
                        case "Negative":
                            {
                                MainModel.Negative(wb);
                                break;
                            }
                        case "ContourSelection":
                            {
                                MainModel.ContourSelection(wb);
                                break;
                            }
                        case "RandomPoints":
                            {
                                MainModel.RandomPoints(wb);
                                break;
                            }
                        case "MedianFiltering":
                            {
                                MainModel.MedianFiltering(wb);
                                break;
                            }
                        case "ToBlack":
                            {
                                MainModel.ToBlack(wb);
                                break;
                            }
                    }
                });               
            }
        }


        private DelegateCommand loadCommand;
        public DelegateCommand Load
        {
            get
            {
                return loadCommand ?? (loadCommand = new DelegateCommand(obj =>
                {
                    OpenFileDialog openDialog = new OpenFileDialog();
                    openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";

                    if (openDialog.ShowDialog() == true)
                    {
                        imagePath = openDialog.FileName;
                        System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath);
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit(); //
                        bitmap.UriSource = new Uri(openDialog.FileName);
                        bitmap.DecodePixelHeight = img.Height;
                        bitmap.DecodePixelWidth = img.Width;
                        bitmap.EndInit();  //
                        Wb = new WriteableBitmap(bitmap);
                    }
                }));
            }
        }

        private DelegateCommand reloadCommand;
        public DelegateCommand Reload
        {
            get
            {
                return reloadCommand ?? (reloadCommand = new DelegateCommand(obj =>
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath);
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit(); //
                    bitmap.UriSource = new Uri(imagePath);
                    bitmap.DecodePixelHeight = img.Height;
                    bitmap.DecodePixelWidth = img.Width;
                    bitmap.EndInit();  //
                    Wb = new WriteableBitmap(bitmap);
                }));
            }
        }

        private DelegateCommand saveCommand;
        public DelegateCommand Save
        {
            get
            {
                return saveCommand ?? (saveCommand = new DelegateCommand(obj =>
                {
                    if(wb != null)
                    {
                        ImageCount = (new DirectoryInfo("ImageLogs")).GetFiles("*.jpg").Count() + 1;
                        String filePath = $"ImageLogs/img_{ImageCount}.jpg";
                        var encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)wb));
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            encoder.Save(stream);
                    }                   
                }));
            }
        }

        private DelegateCommand openFolderCommand;
        public DelegateCommand OpenFolder
        {
            get
            {
                return openFolderCommand ?? (openFolderCommand = new DelegateCommand(obj =>
                {
                    OpenFileDialog openDialog = new OpenFileDialog();
                    openDialog.InitialDirectory = Environment.CurrentDirectory + @"\ImageLogs";

                    openDialog.ShowDialog();
                }));
            }
        }

        private DelegateCommand writeMemoryCommand;
        public DelegateCommand WriteMemory
        {
            get
            {
                return writeMemoryCommand ?? (writeMemoryCommand = new DelegateCommand(obj =>
                {
                    var bytes = wb.ToByteArray();
                    int size = bytes.Length;
                    MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryFile", size * 2 + 4);
                    using (var writer = sharedMemory.CreateViewAccessor(0, size * 2 + 4))
                    {
                        writer.Write(0, size);
                        writer.Write(4, wb.PixelWidth);
                        writer.Write(8, wb.PixelHeight);
                        writer.WriteArray<byte>(12, bytes, 0, size);
                    }
                }));
            }
        }

        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

    }
}
