using FakeItEasy;
using TechiesMoneyExchange.Core.Infrastructure.Navigation;
using TechiesMoneyExchange.Core.UseCases;
using TechiesMoneyExchange.Infrastructure.ExternalServices;
using TechiesMoneyExchange.Model;
using Xunit;

namespace TechiesMoneyExchange.Tests
{
    public class RegisterExchangeOperationUseCaseTests
    {
        [Fact]
        public async Task Given_an_exchangeRequest_Then_Execute_Call_RegisterService_and_NavigationService()
        {
            //Arrange
            var exchangeRateService = A.Fake<IExchangeRateService>();
            var navigationService = A.Fake<INavigationService>();            
            
            var exchangeRate        = new PublishedExchangeRate(Guid.NewGuid(),
                DateTime.UtcNow,
                TimeSpan.FromMinutes(5),
                new Currency { Id = 1, Name = "USD", Symbol = "$"},
                new Currency { Id = 2, Name = "PEN", Symbol = "S/." },
                3.95m, 4.10m);

            var sendingAccount      = A.Dummy<BankAccount>();
            var recievingAccount    = A.Dummy<BankAccount>();
            const decimal sendingAmount = 250m;
            var context = A.Fake<IRegisterExchangeOperationContext>();
            A.CallTo(()=> context.PublishedExchangeRate).Returns(exchangeRate);
            A.CallTo(() => context.SendingAmount).Returns(sendingAmount);
            A.CallTo(() => context.IsBuying).Returns(true);
            A.CallTo(() => context.SendingAccount).Returns(sendingAccount);
            A.CallTo(() => context.RecievingAccount).Returns(recievingAccount);

            var useCase = new RegisterExchangeOperationUseCase(exchangeRateService, navigationService);
            
            //Act
            await useCase.Execute(context);

            //Assert
            A.CallTo(() => navigationService.NavigateTo(Pages.ConfirmationExchange,A<Dictionary<string, object>>.Ignored,false)).MustHaveHappenedOnceExactly();
        }
    }
}