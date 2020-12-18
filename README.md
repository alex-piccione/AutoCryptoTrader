# AutoCryptoTrader

**The goal of this project is just to learn and practice F#.**  
   
The project is a bot for trading on a cryptocurrency markets.

## How it works

The running process buy a predefined amount of tokens at the current market price.  
It then creates a sell limit order on the market at a calculated price.  
The sell price is calculated using a configurable percentage.  
When the sell is executed the program wait for a specific amount of time and then repeats the process.  

### when to buy ?

This logic is not defined yet.  
When the price goes down of a P percentage after X seconds ?

## Simulation

It is possible to run the program with a specific configuration over a specific period of time in the past.  


### Logging  
On every run of the program a new log file is generated.  


## Use cases

T1 - buy at 1.20  



/*
[order_created] (1558211218) 3321371541: 0.4809 BTC @ 7205.76 USD BUY
[order_created] (1558211218) 3321371543: 0.04784502 BTC @ 7233.84 USD BUY
[order_deleted] (1558211199) 3321369898: 0.5 BTC @ 7260.8 USD SELL
[order_deleted] (1558211218) 3321371543: 0.04785 BTC @ 7233.84 USD BUY
[order_created] (1558211219) 3321371582: 0.04783279 BTC @ 7235.69 USD BUY
[order_created] (1558211219) 3321371595: 1.85402142 BTC @ 7233.96 USD BUY
[order_deleted] (1558211219) 3321371582: 0.04783279 BTC @ 7235.69 USD BUY
[order_created] (1558211219) 3321371617: 0.04783286 BTC @ 7235.68 USD BUY
[order_deleted] (1558211219) 3321371617: 0.04783286 BTC @ 7235.68 USD BUY
[order_created] (1558211220) 3321371658: 0.04783273 BTC @ 7235.7 USD BUY
*/
