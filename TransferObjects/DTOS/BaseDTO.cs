using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.Utils;
namespace TransferLayer.DTOS
{
    public class BaseDTO
    {
        [Key]
        public virtual Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set;}

        public bool IsTranscient() => Id == Guid.Empty;

        /// <summary>
        /// See https://github.com/stackify/stackify-api-dotnet/blob/master/Src/StackifyLib/Utils/SequentialGuid.cs
        /// </summary>
        /// <param name="type"></param>
        public void GenerateNewIdentity()
        {
            if (IsTranscient())
            {
                Id = SequentialGuid.NewGuid();
            }
        }

        public void GenerateNewIdentity(Type type)
        {
            if(IsTranscient())
            {
                Id = SequentialGuid.NewGuid();
            }
        }

        public void ChangeCurrentIdentity(Guid id)
        {
            if(id != Guid.Empty)
            {
                Id = id;
            }
        }
    }
}
