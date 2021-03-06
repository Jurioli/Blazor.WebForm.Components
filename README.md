# Blazor.WebForm.Components
 ASP.NET Web Forms System.Web.UI.WebControls Razor Components For Blazor WebAssembly, Blazor Hybrid, Blazor Server.

Demo: <https://blazorwebformdemo.github.io/>

[![NuGet version (Blazor.WebForm.Components)](https://img.shields.io/nuget/v/Blazor.WebForm.Components)](https://www.nuget.org/packages/Blazor.WebForm.Components/)

<pre style="background-color: #eeeeee; border: 1px solid rgb(221, 221, 221); box-sizing: border-box; color: #333333; font-family: &quot;Source Code Pro&quot;, Consolas, Courier, monospace; font-size: 15px; line-height: 22px; margin-bottom: 22px; margin-top: 22px; max-width: 100%; overflow: auto; padding: 4.5px 11px;"><code class="language-cs hljs" style="background-attachment: initial; background-clip: initial; background-image: initial; background-origin: initial; background-position: initial; background-repeat: initial; background-size: initial; border-radius: 0px; border: none; display: block; font-family: &quot;Source Code Pro&quot;, Consolas, Courier, monospace; font-size: 1em; line-height: inherit; margin: 0px; overflow-x: auto; padding: 0px; text-size-adjust: none;">@using System.Web.UI
@using System.Web.UI.WebControls
@page "/fetchdata-gridview"
@inherits ControlComponent
@inject HttpClient Http

&lt;div&gt;
    &lt;h1&gt;Weather forecast (GridView)&lt;/h1&gt;
    &lt;asp.Button Text="Load Data" OnClick="this.Button_Click"&gt;&lt;/asp.Button&gt;
    &lt;hr /&gt;
    &lt;asp.Label @ref="this.label"&gt;&lt;/asp.Label&gt;
    &lt;br /&gt;
    &lt;asp.GridView @ref="this.gridview" AutoGenerateColumns="false" CssClass="table" AllowPaging="true"
                  PageSize="2" OnPageIndexChanging="this.GridView_PageIndexChanging"&gt;
        &lt;Columns&gt;
            &lt;asp.BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:yyyy/M/d}"&gt;&lt;/asp.BoundField&gt;
            &lt;asp.BoundField HeaderText="Temp. (C)" DataField="TemperatureC"&gt;&lt;/asp.BoundField&gt;
            &lt;asp.BoundField HeaderText="Temp. (F)" DataField="TemperatureF"&gt;&lt;/asp.BoundField&gt;
            &lt;asp.BoundField HeaderText="Summary" DataField="Summary"&gt;&lt;/asp.BoundField&gt;
        &lt;/Columns&gt;
    &lt;/asp.GridView&gt;
&lt;/div&gt;

@code {
    private WeatherForecast[] forecasts;
    private Label label;
    private GridView gridview;

    protected async void Button_Click(object sender, EventArgs e)
    {
        forecasts = await Http.GetFromJsonAsync&lt;WeatherForecast[]&gt;("sample-data/weather.json");

        label.Text = DateTime.Now.ToString();

        gridview.PageIndex = 0;
        gridview.DataSource = forecasts;
        gridview.DataBind();

        this.RequestRefresh();
    }

    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridview.PageIndex = e.NewPageIndex;
        gridview.DataBind();
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF =&gt; 32 + (int)(TemperatureC / 0.5556);
    }
}</code></pre>

<table>
    <tr>
        <th align="left" colspan="5"><li>Standard Toolbox</li></th>
    </tr>
    <tr>
        <td>BulletedList</td><td>DropDownList</td><td>ImageMap</td><td>MultiView</td><td>TextBox</td>
    </tr>
    <tr>
        <td>Button</td><td>FileUpload</td><td>Label</td><td>Panel</td><td>Wizard</td>
    </tr>
    <tr>
        <td>Calendar</td><td>HiddenField</td><td>LinkButton</td><td>PlaceHolder</td><td></td>
    </tr>
    <tr>
        <td>CheckBox</td><td>HyperLink</td><td>ListBox</td><td>RadioButton</td><td></td>
    </tr>
    <tr>
        <td>CheckBoxList</td><td>Image</td><td>Literal</td><td>RadioButtonList</td><td></td>
    </tr>
    <tr>
        <td>ComboBox</td><td>ImageButton</td><td>Localize</td><td>Table</td><td></td>
    </tr>
</table>
<p></p>
<table>
    <tr>
        <th align="left" colspan="2"><li>Data Toolbox</li></th>
    </tr>
    <tr>
        <td>DataList</td><td>ListView</td>
    </tr>
    <tr>
        <td>DataPager</td><td>Repeater</td>
    </tr>
    <tr>
        <td>DetailsView</td><td></td>
    </tr>
    <tr>
        <td>FormView</td><td></td>
    </tr>
    <tr>
        <td>GridView</td><td></td>
    </tr>
</table>
<p></p>
<table>
    <tr>
        <th align="left" colspan="2"><li>Validation Toolbox</li></th>
    </tr>
    <tr>
        <td>CompareValidator</td><td>ValidationSummary</td>
    </tr>
    <tr>
        <td>CustomValidator</td><td></td>
    </tr>
    <tr>
        <td>RangeValidator</td><td></td>
    </tr>
    <tr>
        <td>RegularExpressionValidator</td><td></td>
    </tr>
    <tr>
        <td>RequiredFieldValidator</td><td></td>
    </tr>
</table>




### Blazor Server

Add CircuitHandler service to Program.cs

<pre style="background-color: #eeeeee; border: 1px solid rgb(221, 221, 221); box-sizing: border-box; color: #333333; font-family: &quot;Source Code Pro&quot;, Consolas, Courier, monospace; font-size: 15px; line-height: 22px; margin-bottom: 22px; margin-top: 22px; max-width: 100%; overflow: auto; padding: 4.5px 11px;"><code class="language-cs hljs" style="background-attachment: initial; background-clip: initial; background-image: initial; background-origin: initial; background-position: initial; background-repeat: initial; background-size: initial; border-radius: 0px; border: none; display: block; font-family: &quot;Source Code Pro&quot;, Consolas, Courier, monospace; font-size: 1em; line-height: inherit; margin: 0px; overflow-x: auto; padding: 0px; text-size-adjust: none;">builder.Services.AddScoped&lt;CircuitHandler, ScriptManagerCircuitHandler&gt;();</code></pre>


<pre style="background-color: #eeeeee; border: 1px solid rgb(221, 221, 221); box-sizing: border-box; color: #333333; font-family: &quot;Source Code Pro&quot;, Consolas, Courier, monospace; font-size: 15px; line-height: 22px; margin-bottom: 22px; margin-top: 22px; max-width: 100%; overflow: auto; padding: 4.5px 11px;"><code class="language-cs hljs" style="background-attachment: initial; background-clip: initial; background-image: initial; background-origin: initial; background-position: initial; background-repeat: initial; background-size: initial; border-radius: 0px; border: none; display: block; font-family: &quot;Source Code Pro&quot;, Consolas, Courier, monospace; font-size: 1em; line-height: inherit; margin: 0px; overflow-x: auto; padding: 0px; text-size-adjust: none;">using Microsoft.AspNetCore.Components.Server.Circuits;
using System.Web.Hosting;

namespace Server
{
    public class ScriptManagerCircuitHandler : CircuitHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public ScriptManagerCircuitHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            ScriptManagerHost.AddScoped(_serviceProvider);
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            ScriptManagerHost.RemoveScoped(_serviceProvider);
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }
    }
}</code></pre>