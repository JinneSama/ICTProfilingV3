using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ICTProfilingV3.Utility
{
    public class ParseInventory : IParseInventory
    {
        private readonly IPPEInventoryService _ppeService;
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://api.openai.com/v1/chat/completions";

        public ParseInventory(IPPEInventoryService ppeService)
        {
            _apiKey = ConfigurationManager.AppSettings["Parser_APIKey"];
            _ppeService = ppeService;
        }
        public async Task<Device> Parse(string ppeNo, string Specs)
        {
            string prompt = $@"
                        Extract structured data from the following inventory description and return it in JSON format.
                        Attempt to infer the 'Device Type' based on the description. If it's unclear, use your best judgment and provide the most probable type. If the 'Device Type' is unidentifiable, set it to 'Unknown.'
                        Always add the Model, and Brand fields. If a field is missing, set it to 'No Property', 'No Model', or 'No Brand' respectively.
                        If one of the Broken down Data is unidentifiable in the output structure Mark it as 'Others.'
                        ---
                        {Specs}
                        ---
                        Example output structure:
                        {{
                            'Device Type': 'Type of device (Desktop Computer, AIO Desktop Computer, Laptop Computer, Tablet Computer, Server, Desktop Computer All In One, Laptop Battery, Laptop Charger, Power Supply Unit, Operating System, Desktop Monitor, Keyboard, Wireless keyboard and mouse combo, Mouse, Desktop Computer & Scanner, Desktop Computer & Printer, Storage Device, Solid State Drive (SSD), Random Access Memory (RAM), System Unit, Memory Card, External Portable SSD, External Hard Disk Drive, Flash Drive, Tablet, Memory Stick, Microsoft Surface Pro 9, Desktop Monitor, Laptop Speaker (Replacement), Laptop Battery Pack (Replacement), Wired USB Keyboard, Wired USB Mouse, Wireless USB Mouse)',
                            'Processor': 'Processor details (if any)',
                            'Memory': 'Memory details (if any)',
                            'Motherboard': 'Motherboard details (if any)',
                            'Board': 'Board details (if any)',
                            'Screen': 'Screen details (if any)',
                            'CDROM': 'CDROM details (if any)',
                            'Storage': 'Storage details (if any)',
                            'Monitor': 'Monitor details (if any)',
                            'Casing': 'Casing details (if any)',
                            'Brand': 'Brand details (if any)',
                            'Model': 'Model details (if any)',
                            'Warranty': 'Warranty details (if any)',
                            'Ports': 'Ports details (if any)',
                            'LAN': 'LAN details (if any)',
                            'Floppy Disk': 'Floppy Disk details (if any)',
                            'CD/DVD': 'CD/DVD details (if any)',
                            'USB': 'USB details (if any)',
                            'PCI': 'PCI details (if any)',
                            'Speaker': 'Speaker details (if any)',
                            'Microphone': 'Microphone details (if any)',
                            'Webcam': 'Webcam details (if any)',
                            'Cam': 'Cam details (if any)',
                            'Power Supply': 'Power Supply details (if any)',
                            'Fans': 'Fans details (if any)',
                            'Cables': 'Cables details (if any)',
                            'Graphics Card': 'Graphics Card details (if any)',
                            'Video Card': 'Video Card details (if any)',
                            'Sound Card': 'Sound Card details (if any)',
                            'Webcam': 'Webcam details (if any)',
                            'Display': 'Webcam details (if any)',
                            'LCD': 'LCD details (if any)',
                            'UPS': 'UPS details (if any)',
                            'Software/System': 'Software/System details (if any)',
                            'AVR': 'AVR details (if any)',
                            'CD Rom': 'CD Rom details (if any)',
                            'Floppy': 'Floppy details (if any)',
                            'Others': '(if any)',
                            'Status': 'Status details (Can\'t Locate, Needs Repair, Condition etc.)',
                            'SN': 'SN details (if any)',
                            'Property No': 'Property No details (if any)',
                            'Accessories': ['List of accessories (Cables,Mouse,Keyboard,Kit,Manual,Bag,USB,Combo,Case,Riser etc.)'],
                            'PRS Date': 'PRS date if mentioned (format: MM/DD/YYYY)'
                        }}
                        Exclude fields that are empty or missing.";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "You are a helpful assistant that formats inventory data." },
                        new { role = "user", content = prompt }
                    }
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(_apiUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();

                JObject result = JObject.Parse(responseString);
                var res = result["choices"]?[0]?["message"]?["content"] != null
                    ? JObject.Parse(result["choices"][0]["message"]["content"].ToString())
                    : null;

                var resString = res.ToString(Formatting.Indented);
                return SaveToData(res, ppeNo, resString);
            }
        }

        private Device SaveToData(JObject data, string ppeNo, string resString)
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(resString);
            var jsonObject = JsonConvert.DeserializeObject<JObject>(resString);

            var device = new Device
            {
                DeviceType = (string)dict["Device Type"],
                Brand = (string)dict["Brand"],
                Model = (string)dict["Model"],
                Specs = ConvertToSpecsList(jsonObject)
            };

            return device;
        }

        public async Task GeneratePPESpecs(Device device, string ppeNo, Model _model)
        {
            var ppe = await _ppeService.GetByFilterAsync(x => x.PropertyNo == ppeNo);
            var ppeSpecs = new PPEsSpecs
            {
                ItemNo = 1,
                Quantity = 1,
                Unit = Models.Enums.Unit.unit,
                UnitCost = (long)ppe.UnitValue,
                TotalCost = (long)ppe.TotalValue,
                ModelId = _model.Id,
                PPEs = ppe
            };

            foreach (var details in device.Specs)
            {
                var ppeDetails = new PPEsSpecsDetails
                {
                    ItemNo = details.ItemNo,
                    Specs = details.SpecsName,
                    Description = details.Description
                };
                ppeSpecs.PPEsSpecsDetails.Add(ppeDetails);
            }
            await _ppeService.PPESpecsBaseService.AddAsync(ppeSpecs);
        }

        private List<Specs> ConvertToSpecsList(JObject jsonObject)
        {
            var specsList = new List<Specs>();
            int index = 1;
            FlattenJson(jsonObject, "", specsList, ref index);
            return specsList;
        }

        private void FlattenJson(JObject jsonObject, string parentKey, List<Specs> specsList, ref int index)
        {
            foreach (var kvp in jsonObject)
            {
                string key = string.IsNullOrEmpty(parentKey) ? kvp.Key : $"{parentKey} - {kvp.Key}";
                if (key == "Device Type" || key == "Model" || key == "Brand") continue;
                if (kvp.Value is JObject nestedObject)
                {
                    FlattenJson(nestedObject, key, specsList, ref index);
                }
                else if (kvp.Value is JArray array)
                {
                    string arrayValue = string.Join(", ", array.ToObject<List<string>>());
                    specsList.Add(new Specs { ItemNo = index++, SpecsName = key, Description = arrayValue });
                }
                else
                {
                    specsList.Add(new Specs { ItemNo = index++, SpecsName = key, Description = kvp.Value.ToString() });
                }
            }
        }

    }
}
