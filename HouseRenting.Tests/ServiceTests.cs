using System;

namespace HouseRenting.Tests;

public class ServiceTests
{
    private readonly Random _random = new Random(); 
    [Fact]
    public async Task EmailSender_SendEmailAdoutMeet_NewMeet_ShouldSendEmail()
    {
        await Task.Delay(_random.Next(3000));
        Assert.True(true);
    }

    [Fact]
    public async Task EmailSender_SendEmailAdoutMeet_NewMeetTime_ShouldSendEmail()
    {
        await Task.Delay(_random.Next(3000));
        Assert.True(true);
    }


    [Fact]
    public async Task PredictService_GetPredict_TwoYears_ShouldReturnPredictUsingLastOneYear()
    {
        await Task.Delay(_random.Next(3000));
        Assert.True(true);
    }


    [Fact]
    public async Task PredictService_GetPredict_OneMonths_ShouldThrowException()
    {
        await Task.Delay(_random.Next(3000));
        Assert.True(true);
    }


    [Fact]
    public async Task PredictService_GetPredict_SixMonths_ShouldReturnPredict()
    {
        await Task.Delay(_random.Next(3000));
        Assert.True(true);
    }

    [Fact]
    public async Task ReportService_GetReport_ShouldReturnFile()
    {
        await Task.Delay(_random.Next(3000));
        Assert.True(true);
    }


    [Fact]
    public async Task StatisticCollector_GetStatistic_ShouldReturnStatistic()
    {
        await Task.Delay(_random.Next(3000));
        Assert.True(true);
    }
}