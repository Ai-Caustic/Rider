using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Utils;

namespace DomainLayer.Models
{
    public abstract class BaseEntity 
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

        public override bool Equals(object ? obj)
        {
            if(!(obj is BaseEntity other ))
            { return false; }

            if(ReferenceEquals(this, other))
            { return true; }

            if(GetType() != other.GetType())
            { return false; }

            if (other.IsTranscient() || IsTranscient())
            { return false; }

            return Id == other.Id;

        }

        public static bool operator == (BaseEntity left, BaseEntity right)
        {
            if (left is null && right is null)
            { return true; }

            if (left is null || right is null)
            { return false; }

            return left.Equals(right);
        }

        public static bool operator != (BaseEntity left, BaseEntity right) => !(left == right);

        public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();

    }
}
        

