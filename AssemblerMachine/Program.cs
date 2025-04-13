using AssemblerMachine.DataAccess;
using AssemblerMachine.Services;
using Microsoft.Extensions.DependencyInjection;
namespace AssemblerMachine
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var services = new ServiceCollection();
            var dbContext = new ApplicationDbContext();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IAssembleService, AssembleService>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<ICarService, CarService>();

            services.AddScoped<Form1>(provider =>
            {
                var creatorService = provider.GetRequiredService<IAssembleService>();
                var CarService = provider.GetRequiredService<ICarService>();
                return new Form1(creatorService , CarService);

            });

            using var serviceProvider = services.BuildServiceProvider();
            var mainForm = serviceProvider.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }
    }
    // retrive data from database then pass it to the dll to assemble it then simply crate new object of car with this data add this obj  
    // 5 wheel , 4 door , 4 glass , 1 motor

    // if numberWheel = 0 continue 
    // else 
    // context.components.where(x => x.type == "door" && x.subtype == steel).Count >= number d5l  
    // true in all what i entered then i will assemble 
    //
    //context.components.where(x => x.type == "wheel").(select x=>x.instruction).tolist;

    // create new object car 
}