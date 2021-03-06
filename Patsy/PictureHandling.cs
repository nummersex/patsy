﻿using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Threading;

namespace Patsy
{
    class PictureHandling
    {
        private string _fileFormatString;

        public void TakePicture(string fileFormatString)
        {
            _fileFormatString = fileFormatString;
            // enumerate video devices
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count < 1)
                return; // no videoDevices was found.

            // create video source
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            // set NewFrame event handler
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);

            // start the video source
            videoSource.Start();

            // 500ms seems like a good enough value for just taking one picture with default settings.
            Thread.Sleep(500);

            // signal to stop when you no longer need capturing
            videoSource.SignalToStop();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // get new frame
            Bitmap bitmap = eventArgs.Frame;
            // save it
            bitmap.Save(_fileFormatString);
        }
    }
}
