using MediatR;
using WMS.WF.Application.Features.Products.Queries.GetByCode;

namespace WMS.WF.UI
{
    public partial class Form1 : Form
    {
        private readonly IMediator _mediator;
        public Form1(IMediator mediator)
        {
            _mediator = mediator;
            InitializeComponent();
        }

        public async Task Init()
        {
            var query = new GetProductByCodeQuery("Code 1");
            var result = await _mediator.Send(query);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Init();
        }
    }
}
