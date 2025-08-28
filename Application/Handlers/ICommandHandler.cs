﻿namespace GeminiTelegramBot.Application.Handlers
{
    public interface ICommandHandler<TCommand, TResult>
    {
        Task<TResult> Handle(TCommand command, CancellationToken token);
    }
}
