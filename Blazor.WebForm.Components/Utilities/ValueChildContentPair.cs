﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.WebForm.UI
{
    internal abstract class ValueChildContentPair
    {
        internal abstract void AddChildContent(RenderTreeBuilder builder, int sequence);
    }

    internal class ValueChildContentPair<TValue> : ValueChildContentPair
    {
        public TValue Value { get; }
        public RenderFragment ChildContent { get; }
        public ValueChildContentPair(TValue value, RenderFragment childContent)
        {
            this.Value = value;
            this.ChildContent = childContent;
        }
        internal override void AddChildContent(RenderTreeBuilder builder, int sequence)
        {
            builder.OpenComponent<CascadingValue<TValue>>(sequence);
            builder.AddAttribute(sequence + 1, "Value", this.Value);
            builder.AddAttribute(sequence + 2, "ChildContent", (RenderFragment)(builder2 =>
            {
                builder2.AddContent(sequence + 3, this.ChildContent);
            }));
            builder.CloseComponent();
        }
    }
}
