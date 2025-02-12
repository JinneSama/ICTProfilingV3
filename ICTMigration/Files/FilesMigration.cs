using Helpers.Interfaces;
using Helpers.NetworkFolder;
using Helpers.Security;
using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Repository;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTMigration.Files
{
    public class FilesMigration : IEncryptFile
    {
        private readonly ICTv2Entities ictv2Model;
        private readonly IUnitOfWork unitOfWork;
        private HTTPNetworkFolder networkFolder;
        public FilesMigration()
        {
            ictv2Model = new ICTv2Entities();
            unitOfWork = new UnitOfWork();
            networkFolder = new HTTPNetworkFolder();
        }
        public async Task MigratePGNImages()
        {
            var requests = ictv2Model.PGNRequestImages.ToList();
            var v3Requests = unitOfWork.PGNRequestsRepo.GetAll().ToList();

            foreach (var request in requests)
            {
                var requestExist = v3Requests.FirstOrDefault(x => x.Id == request.fldRequestId);
                if (requestExist == null) continue;

                var path = $@"C:\Users\Scott\Desktop\PGNDocuments\{request.fldImagePath}.jpeg";
                var checkFile = File.Exists(path);
                if (!checkFile) continue;
                var image = Image.FromFile(path);
                var securityStamp = Guid.NewGuid().ToString();
                var documentData = EncryptFile(request.fldImagePath);
                var doc = new PGNDocuments
                {
                    SecurityStamp = documentData.securityStamp,
                    FileName = documentData.filename + ".jpeg",
                    DocOrder = (int)request.fldImagePage,
                    PGNRequestId = (int)request.fldRequestId
                };
                await networkFolder.UploadFile(image, doc.FileName);
                unitOfWork.PGNDocumentsRepo.Insert(doc);
            }
            await unitOfWork.SaveChangesAsync();
        }

        public EncryptionData EncryptFile(string filename)
        {
            var securityStamp = Guid.NewGuid().ToString();
            return new EncryptionData(Cryptography.Encrypt(filename, securityStamp), securityStamp);
        }
    }
}
