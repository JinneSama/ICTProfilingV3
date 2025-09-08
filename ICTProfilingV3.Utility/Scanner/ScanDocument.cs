using WIA;
using System.Collections.Generic;
using System.IO;
using System;
using System.Drawing;
using ICTProfilingV3.Interfaces;

namespace ICTProfilingV3.Utility.Scanner
{
    public class ScanDocument : IScanDocument
    {
        public List<Image> ScanImages()
        {
            try
            {
                var deviceManager = new DeviceManager();

                var ret = new List<Image>();

                var dialog = new CommonDialog();
                var device = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType);
                if (device == null)
                    return null;
                var items = dialog.ShowSelectItems(device);
                if(items == null) 
                    return null;
                var hasPages = true;

                foreach (Item item in items)
                {
                    while (hasPages)
                    {
                        try
                        {
                            var image = (ImageFile)dialog.ShowTransfer(item);
                            if (image != null && image.FileData != null)
                            {
                                var imageBytes = (byte[])image.FileData.get_BinaryData();
                                var ms = new MemoryStream(imageBytes);
                                Image img = null;
                                img = Image.FromStream(ms);

                                ret.Add(img);
                            }

                            Property documentHandlingSelect = null;
                            Property documentHandlingStatus = null;
                            foreach (Property prop in device.Properties)
                            {
                                if (prop.PropertyID == WIAScanner.WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                                    documentHandlingSelect = prop;
                                if (prop.PropertyID == WIAScanner.WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                                    documentHandlingStatus = prop;
                            }
                            hasPages = false;
                            if (documentHandlingSelect != null)
                                if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) &
                                     WIAScanner.WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER) != 0)
                                    hasPages = (Convert.ToUInt32(documentHandlingStatus.get_Value()) &
                                                WIAScanner.WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0;
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                    
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
