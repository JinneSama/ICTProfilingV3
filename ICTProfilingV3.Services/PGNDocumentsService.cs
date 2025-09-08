using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class PGNDocumentsService : DocumentService<PGNDocuments, int>, IDocumentService<PGNDocuments, int>
    {
        public PGNDocumentsService(IRepository<int, PGNDocuments> baseRepo, IHTTPNetworkFolder networkFolder, 
            IScanDocument scanDocument) : base(baseRepo, networkFolder, scanDocument)
        {
        }

        public override async Task<IEnumerable<EncryptionData>> ScanFile(string docNamePrefix, int parentId)
        {
            var docData = await base.ScanFile(docNamePrefix, parentId);

            int docOrder = 1;
            var docs = base.GetAll().Where(x => x.PGNRequestId == parentId).ToList();
            if (docs.LastOrDefault() != null) docOrder = docs.LastOrDefault().DocOrder + 1;

            foreach (var doc in docData) {
                var document = new PGNDocuments
                {
                    FileName = doc.filename,
                    SecurityStamp = doc.securityStamp,
                    PGNRequestId = parentId,
                    DocOrder = docOrder
                };
                docOrder++;
                await base.AddAsync(document);
            }
            return docData;
        }

        public override async Task DeleteRangeAsync(Expression<Func<PGNDocuments, bool>> filter)
        {
            var images = base.GetAll().Where(filter).ToList();
            foreach (var image in images)
            {
                await base.DeleteImage(image.FileName + ".jpeg", image.Id, image.PGNRequestId);
            }
            //await base.DeleteRangeAsync(filter);
        }
        public override async Task DeleteImage(string docName, int Id, int parentId)
        {
            await base.DeleteImage(docName, Id, parentId);
            await base.DeleteAsync(Id);
            await ReOrderDocument(parentId, x => x.PGNRequestId, x => x.DocOrder);
        }
    }
}
