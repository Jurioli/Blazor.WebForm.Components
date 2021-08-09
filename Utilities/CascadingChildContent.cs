using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI

{
    internal abstract class CascadingChildContentBase
    {
        public ValueChildContentPair[] ChildContent { get; set; }
        public int ChildLevel { get; set; }
        public void RenderFragment(RenderTreeBuilder builder)
        {
            if (this.ChildContent != null && this.ChildContent.Length > 0)
            {
                for (int i = 0; i < this.ChildContent.Length; i++)
                {
                    this.ChildContent[i].AddChildContent(builder, i * 4);
                }
                int sequence = this.ChildContent.Length * 4;
                builder.OpenComponent<RenderContent>(sequence);
                builder.AddAttribute(sequence + 1, "ChildContent", CascadingChildContent.StackContent(this.ChildLevel, sequence + 2, builder2 =>
                {
                    this.AfterContent(builder2, sequence + 2 + (this.ChildLevel * 2));
                }));
                builder.CloseComponent();
            }
            else
            {
                this.AfterContent(builder, 0);
            }
        }
        protected abstract void AfterContent(RenderTreeBuilder builder, int sequence);
    }

    internal class CascadingChildContent : CascadingChildContentBase
    {
        public Func<RenderFragment> Render { get; set; }
        protected override void AfterContent(RenderTreeBuilder builder, int sequence)
        {
            builder.AddContent(sequence, this.Render.Invoke());
        }
        internal static RenderFragment StackContent(int childLevel, int sequence, RenderFragment first)
        {
            if (childLevel == 0)
            {
                return first;
            }
            Stack<RenderFragment> queue = new Stack<RenderFragment>();
            queue.Push(first);
            while (childLevel > 0)
            {
                queue.Push(builder =>
                {
                    builder.OpenComponent<RenderContent>(sequence++);
                    builder.AddAttribute(sequence++, "ChildContent", queue.Pop());
                    builder.CloseComponent();
                });
                childLevel -= 1;
            }
            return queue.Pop();
        }
    }

    internal class ActionCascadingChildContent : CascadingChildContentBase
    {
        public Action Render { get; set; }
        protected override void AfterContent(RenderTreeBuilder builder, int sequence)
        {
            this.Render.Invoke();
        }
    }

    //internal abstract class NotCascadingChildContentBase
    //{
    //    public RenderFragment[] ChildContent { get; set; }
    //    public int ChildLevel { get; set; }
    //    public void RenderFragment(RenderTreeBuilder builder)
    //    {
    //        if (this.ChildContent != null)
    //        {
    //            for (int i = 0; i < this.ChildContent.Length; i++)
    //            {
    //                this.AddChildContent(builder, i, this.ChildContent[i]);
    //            }
    //            int sequence = this.ChildContent.Length;
    //            builder.AddContent(sequence, CascadingChildContent.StackContent(this.ChildLevel, sequence + 1, builder2 =>
    //            {
    //                this.AfterContent(builder2, sequence + 1 + (this.ChildLevel * 2));
    //            }));
    //        }
    //        else
    //        {
    //            this.AfterContent(builder, 0);
    //        }
    //    }
    //    private void AddChildContent(RenderTreeBuilder builder, int sequence, RenderFragment childContent)
    //    {
    //        builder.AddContent(sequence, childContent);
    //    }
    //    protected abstract void AfterContent(RenderTreeBuilder builder, int sequence);
    //}

    //internal class NotCascadingChildContent : NotCascadingChildContentBase
    //{
    //    public Func<RenderFragment> Render { get; set; }
    //    protected override void AfterContent(RenderTreeBuilder builder, int sequence)
    //    {
    //        builder.AddContent(sequence, this.Render.Invoke());
    //    }
    //}

    //internal class ActionNotCascadingChildContent : NotCascadingChildContentBase
    //{
    //    public Action Render { get; set; }
    //    protected override void AfterContent(RenderTreeBuilder builder, int sequence)
    //    {
    //        this.Render.Invoke();
    //    }
    //}

    internal class RenderContent : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            if (this.ChildContent != null)
            {
                builder.AddContent(0, this.ChildContent);
            }
        }
    }
}
