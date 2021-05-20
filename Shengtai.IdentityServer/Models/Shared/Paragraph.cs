using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public class Paragraph
    {
        // 不可為空
        public string Text { get; set; }

        // 可為空
        public string Small { get; set; }

        // 可為空
        //public Badge Badge { get; set; }

        public Paragraph Parent { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Paragraph another)
            {
                if (another.Parent == null && this.Parent == null)
                {
                    if (another.Text == this.Text && another.Small == this.Small)
                        return true;
                }
                else if (another.Parent != null && this.Parent != null)
                {
                    if (another.Parent.Text == this.Parent.Text && another.Parent.Small == this.Parent.Small && another.Text == this.Text && another.Small == this.Small)
                        return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;

            if (this.Parent != null)
            {
                hashCode += this.Parent.Text.GetHashCode();
                if (!string.IsNullOrEmpty(this.Parent.Small))
                    hashCode += this.Parent.Small.GetHashCode();
            }

            if (!string.IsNullOrEmpty(this.Text))
                hashCode += this.Text.GetHashCode();

            if (!string.IsNullOrEmpty(this.Small))
                hashCode += this.Small.GetHashCode();

            return hashCode;
        }
    }
}
