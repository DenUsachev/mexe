package main

import (
	"MarketConnector/api"
	log "github.com/sirupsen/logrus"
)

func main() {
	log.SetFormatter(&log.JSONFormatter{})
	log.SetLevel(log.TraceLevel)
	var okx = api.New(
		"",
		"https://www.okx.com",
		"",
		"",
		"")
	log.Debugf("Maker EXecution Engine initializing: [%s]", okx.ClientId)
	okx.Test()
	hasBalance, balance := okx.GetBalance()
	if hasBalance {
		log.Debugf("Has account summary: %s", balance)
	} else {
		log.Fatal("Cannot fetch data from OKX...")
	}
}
