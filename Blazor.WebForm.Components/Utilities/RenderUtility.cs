using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Blazor.WebForm.UI
{
    internal static class RenderUtility
    {
        public static RenderFragment RenderWithCascading<TValue>(TValue value, RenderFragment childContent, int childLevel, Func<TValue, RenderFragment> render)
        {
            return RenderWithCascading(value, childContent, childLevel, () => render.Invoke(value));
        }

        public static RenderFragment RenderWithCascading<TValue>(TValue value, RenderFragment childContent, int childLevel, Func<RenderFragment> render)
        {
            return RenderWithCascading(new ValueChildContentPair[] { new ValueChildContentPair<TValue>(value, childContent) }, childLevel, render);
        }

        private static RenderFragment RenderWithCascading(ValueChildContentPair[] childContent, int childLevel, Func<RenderFragment> render)
        {
            CascadingChildContent content = new CascadingChildContent();
            content.ChildContent = childContent;
            content.ChildLevel = childLevel;
            content.Render = render;
            return new RenderFragment(content.RenderFragment);
        }

        public static RenderFragment RenderWithCascading<TValue>(TValue value, RenderFragment childContent, int childLevel, Action render)
        {
            return RenderWithCascading(new ValueChildContentPair[] { new ValueChildContentPair<TValue>(value, childContent) }, childLevel, render);
        }

        private static RenderFragment RenderWithCascading(ValueChildContentPair[] childContent, int childLevel, Action render)
        {
            ActionCascadingChildContent content = new ActionCascadingChildContent();
            content.ChildContent = childContent;
            content.ChildLevel = childLevel;
            content.Render = render;
            return new RenderFragment(content.RenderFragment);
        }

        //public static RenderFragment Render(RenderFragment childContent, int childLevel, Func<RenderFragment> render)
        //{
        //    return Render(new RenderFragment[] { childContent }, childLevel, render);
        //}

        //private static RenderFragment Render(RenderFragment[] childContent, int childLevel, Func<RenderFragment> render)
        //{
        //    NotCascadingChildContent content = new NotCascadingChildContent();
        //    content.ChildContent = childContent;
        //    content.ChildLevel = childLevel;
        //    content.Render = render;
        //    return new RenderFragment(content.RenderFragment);
        //}

        //public static RenderFragment Render(RenderFragment childContent, int childLevel, Action render)
        //{
        //    return Render(new RenderFragment[] { childContent }, childLevel, render);
        //}

        //private static RenderFragment Render(RenderFragment[] childContent, int childLevel, Action render)
        //{
        //    ActionNotCascadingChildContent content = new ActionNotCascadingChildContent();
        //    content.ChildContent = childContent;
        //    content.ChildLevel = childLevel;
        //    content.Render = render;
        //    return new RenderFragment(content.RenderFragment);
        //}

        public static string GetContentString(RenderFragment childContent)
        {
            TryGetContentString(childContent, out string value);
            return value;
        }

        public static void SetContentString(ITextContentAccessor textContentAccessor, RenderFragment childContent)
        {
            if (TryGetContentString(childContent, out string text))
            {
                textContentAccessor.Text = text;
            }
            else
            {
                textContentAccessor.TextContent = childContent;
            }
        }

        private static bool TryGetContentString(RenderFragment childContent, out string value)
        {
            try
            {
                using RenderTreeBuilder builder = new RenderTreeBuilder();
                childContent.Invoke(builder);
                List<string> texts = new List<string>();
                foreach (RenderTreeFrame frame in builder.GetFrames().Array)
                {
                    switch (frame.FrameType)
                    {
                        case RenderTreeFrameType.None:
                            break;
                        case RenderTreeFrameType.Text:
                            texts.Add(frame.TextContent);
                            break;
                        case RenderTreeFrameType.Markup:
                            texts.Add(frame.MarkupContent);
                            break;
                        default:
                            value = null;
                            return false;
                    }
                }
                value = string.Concat(texts);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }
    }
}
