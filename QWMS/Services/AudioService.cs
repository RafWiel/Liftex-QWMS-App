using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using QWMS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Services
{
    public class AudioService : IAudioService
    {
        private readonly IAudioManager _audioManager;
        private ILogger<AudioService> _logger;
        private IAudioPlayer? _beepSoundPlayer;
        private IAudioPlayer? _errorSoundPlayer;
        private IAudioPlayer? _notificationSoundPlayer;
        private IAudioPlayer? _successSoundPlayer;

        public AudioService(IAudioManager audioManager, ILogger<AudioService> logger)
        {
            _audioManager = audioManager;
            _logger = logger;

            Initialize();            
        }

        private async void Initialize()
        {
            try
            {
                _beepSoundPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Sounds/beep.wav"));
                _errorSoundPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Sounds/error.wav"));
                _notificationSoundPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Sounds/notification.wav"));
                _successSoundPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Sounds/success.wav"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public void PlayBeepSound()
        {
            _beepSoundPlayer?.Play();
        }

        public void PlayErrorSound()
        {
            _errorSoundPlayer?.Play();
        }

        public void PlayNotificationSound()
        {
            _notificationSoundPlayer?.Play();
        }

        public void PlaySuccessSound()
        {
            _successSoundPlayer?.Play();
        }
    }
}
