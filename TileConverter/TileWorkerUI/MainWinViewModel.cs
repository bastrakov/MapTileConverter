
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TileWorker;


namespace TileWorkerUI
{
		public class MainWinViewModel : INotifyPropertyChanged
		{

				private bool _canExecute = true;
				private bool _isWorking = false;

				public event PropertyChangedEventHandler PropertyChanged;

				protected virtual void OnPropertyChanged([CallerMemberName]
				string propertyName = null)
				{
						PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
				}


				#region buttons commands

				private ICommand _openInDirCommand;
				public ICommand OpenInDirCommand
				{
						get {
								return _openInDirCommand ?? (_openInDirCommand = new CommandHandler(() => SelectInDir(), _canExecute));
						}
				}

				private ICommand _openOutDirCommand;
				public ICommand OpenOutDirCommand
				{
						get
						{
								return _openOutDirCommand ?? (_openOutDirCommand = new CommandHandler(() => SelectOutDir(), _canExecute));
						}
				}

				private ICommand _convertCommand;
				public ICommand ConvertCommand
				{
						get
						{
								return _convertCommand ?? (_convertCommand = new CommandHandler(() => DoConvert(), _canExecute));
						}
				}

				#endregion


				#region text fields

				private string _inDir;

				public string InDir
				{
						get { return _inDir; }
						set
						{
								_inDir = value;
								OnPropertyChanged("InDir");
						}
				}

				private string _inFileMask;

				public string InFileMask
				{
						get { return _inFileMask; }
						set
						{
								_inFileMask = value;
								OnPropertyChanged("InFileMask");
						}
				}


				private string _outDir;

				public string OutDir
				{
						get { return _outDir; }
						set
						{
								_outDir = value;
								OnPropertyChanged("OutDir");
						}
				}


				private string _outFileMask;

				public string OutFileMask
				{
						get { return _outFileMask; }
						set
						{
								_outFileMask = value;
								OnPropertyChanged("OutFileMask");
						}
				}

				private string _reportMsg;

				public string ReportMsg
				{
						get { return _reportMsg; }
						set
						{
								_reportMsg = value;
								OnPropertyChanged("ReportMsg");
						}
				}

				#endregion


				#region ConvertTypes radio

				private ConvertTypes _convertType = ConvertTypes.SphericalToWgs84;

				public ConvertTypes ConvertType
				{
						get { return _convertType; }
						set
						{
								if (_convertType == value)
										return;

								_convertType = value;
								OnPropertyChanged("SphericalToWgs84");
								OnPropertyChanged("Wgs84ToSpherical");
						}
				}

				public bool SphericalToWgs84
				{
						get { return ConvertType == ConvertTypes.SphericalToWgs84; }
						set { ConvertType = value ? ConvertTypes.SphericalToWgs84 : ConvertType; }
				}

				public bool Wgs84ToSpherical
				{
						get { return ConvertType == ConvertTypes.Wgs84ToSpherical; }
						set { ConvertType = value ? ConvertTypes.Wgs84ToSpherical : ConvertType; }
				}

				#endregion


				public void DoConvert()
				{
						if (!_isWorking && CheckAllData()) 
						{
								if (SphericalToWgs84)
										new TileConverter().ConvertFromSphericalToWgs84(InDir, InFileMask, OutDir, OutFileMask);
								else 
										new TileConverter().ConvertFromWgs84ToSpherical(InDir, InFileMask, OutDir, OutFileMask);

								ReportMsg = "DONE";
						} 
				}

				public void SelectInDir()
				{
						var msgString = "Folder selection";

						var ofd = new OpenFileDialog();

						ofd.FileName = msgString;
						ofd.CheckPathExists = true;
						ofd.ShowReadOnly = false;
						ofd.ReadOnlyChecked = true;
						ofd.CheckFileExists = false;
						ofd.ValidateNames = false;

						if (ofd.ShowDialog() == true)
						{
								InDir = ofd.FileName.Replace(msgString, "");
						}
				}

				public void SelectOutDir()
				{
						var msgString = "Folder selection";

						var ofd = new OpenFileDialog();

						ofd.FileName = msgString;
						ofd.CheckPathExists = true;
						ofd.ShowReadOnly = false;
						ofd.ReadOnlyChecked = true;
						ofd.CheckFileExists = false;
						ofd.ValidateNames = false;

						if (ofd.ShowDialog() == true)
						{
								OutDir = ofd.FileName.Replace(msgString, "");
						}
				}
		
				private bool CheckAllData()	
				{
						var inDirOk = !string.IsNullOrWhiteSpace(InDir);
						if (!inDirOk) {
								ReportMsg = "no IN directory";
								return false;
						}

						var inMaskOk = !string.IsNullOrWhiteSpace(InFileMask)
														&& InFileMask.Contains(@"{0}")
														&& InFileMask.Contains(@"{1}")
														&& InFileMask.Contains(@"{2}");
						if (!inMaskOk) {
								ReportMsg = "no MASK for a tile file (it needs {0},{1},{2})";
								return false;
						}

						var outDirOk = !string.IsNullOrWhiteSpace(OutDir);
						if (!outDirOk)
						{
								ReportMsg = "no OUT directory";
								return false;
						}

						var outMaskOk = !string.IsNullOrWhiteSpace(OutFileMask)
														&& OutFileMask.Contains(@"{0}")
														&& OutFileMask.Contains(@"{1}")
														&& OutFileMask.Contains(@"{2}");
						if (!outMaskOk)
						{
								ReportMsg = "no MASK for an OUT tile file (it needs {0},{1},{2})";
								return false;
						}

						ReportMsg = "all OK";

						return true;
				}


		}

		public enum ConvertTypes
		{
				SphericalToWgs84,
				Wgs84ToSpherical
		}

}
