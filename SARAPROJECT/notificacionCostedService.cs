using Microsoft.AspNetCore.SignalR;
using SARAPROJECT.Models;

namespace SARAPROJECT
{
    public class notificacionCostedService : IHostedService, IDisposable
    {
        private readonly IHubContext<VentasHub> _ventashub; 
        private Timer _timer;


        public notificacionCostedService(IHubContext<VentasHub> ventashub)
        {
            _ventashub = ventashub;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendInfo, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

            return Task.CompletedTask;

        } 
        private void SendInfo(object state)
        {
            IEnumerable<Ventum> venta = null;
            using (var context = new SARADBContext())
            {
                venta = context.Venta.Where(v => v.IdEstventa == 1).ToList();
            }
            _ventashub.Clients.All.SendAsync("Receive", venta);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
