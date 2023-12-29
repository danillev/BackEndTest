
using BackEndTest.Services;

var webAppBuilder = WebApplication.CreateBuilder(args);
var serviceFacade = new ServiceFacade(webAppBuilder);
serviceFacade.Run();
