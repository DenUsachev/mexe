package api

import (
	"crypto/hmac"
	"crypto/sha256"
	"encoding/base64"
	"fmt"
	"github.com/go-resty/resty/v2"
	log "github.com/sirupsen/logrus"
	"time"
)

var rClient *resty.Client

// Example: 2020-12-08T09:08:57.715Z
// https://www.okx.com/docs-v5/en/?shell#overview-rest-authentication-making-requests
const OkxTimeFormat = "2006-01-02T15:04:05.000Z"

type OkxConnection struct {
	ClientId     string
	ConnectionId string
	ApiUri       string
	ApiKey       string
	Secret       string
	Passphrase   string
}

func (okx OkxConnection) createSign(method string, endpoint string) (string, string) {
	log.Trace("Creating signature")
	currentTs := time.Now().Format(OkxTimeFormat)
	log.Trace("Having timestamp: " + currentTs)
	stringToHash := fmt.Sprintf("%s%s%s", currentTs, method, endpoint)
	hash := hmac.New(sha256.New, []byte(okx.Secret))
	hash.Write([]byte(stringToHash))
	var sign = base64.URLEncoding.EncodeToString(hash.Sum(nil))
	return sign, currentTs
}

func (okx OkxConnection) doHttpPostRequest(endpoint string, data interface{}) (bool, error, *ApiResponse) {
	log.Tracef("Sending HTTP POST: %s%s", okx.ApiUri, endpoint)
	var res ApiResponse
	var response, err = rClient.R().
		SetResult(&res).
		SetBody(data).
		ForceContentType("application/json").
		Post(okx.ApiUri + endpoint)
	log.Tracef("Response Status: %d = %s", response.StatusCode(), response.Status())
	if response != nil && response.StatusCode() == 200 {
		return true, nil, &res
	}
	return false, err, nil
}

func New(clientName string, uri string, key string, secret string, passphrase string) OkxConnection {
	conn := OkxConnection{
		ClientId:     clientName,
		ConnectionId: "",
		ApiUri:       uri,
		ApiKey:       key,
		Secret:       secret,
		Passphrase:   passphrase,
	}
	rClient = resty.New()
	rClient.OnBeforeRequest(func(c *resty.Client, req *resty.Request) error {
		req.Header["OK-ACCESS-KEY"] = []string{conn.ApiKey}
		req.Header["OK-ACCESS-PASSPHRASE"] = []string{conn.Passphrase}
		req.Header["OK-ACCESS-SIGN"] = []string{c.Header.Get("Ok-Access-Sign")}
		req.Header["OK-ACCESS-TIMESTAMP"] = []string{c.Header.Get("Ok-Access-Timestamp")}
		return nil
	})
	return conn
}

func (okx OkxConnection) SignRequest(request *resty.Request, method string, endpoint string) bool {
	if request == nil {
		return false
	}
	var sign, timestamp = okx.createSign(HttpMethodGet, BalanceEndpoint)
	rClient.SetHeader("Ok-Access-Sign", sign)
	rClient.SetHeader("Ok-Access-Timestamp", timestamp)
	return true
}

func (okx OkxConnection) GetBalance() (bool, *BalanceResponse) {
	log.Tracef("Sending HTTP GET: %s%s", okx.ApiUri, BalanceEndpoint)
	var br BalanceResponse
	var restRequest = rClient.R().SetResult(&br)
	var signResult = okx.SignRequest(restRequest, HttpMethodGet, BalanceEndpoint)
	if !signResult {
		log.Error("Could not create sign for request")
		return false, nil
	}
	var resp, _ = restRequest.Get(okx.ApiUri + BalanceEndpoint)
	log.Tracef("Response Status: %s", resp.Status())
	if resp != nil && resp.StatusCode() == 200 {
		return true, &br
	}
	return false, nil
}

func (okx OkxConnection) Test() {
	log.Debug("API test procedure")
	var sign, _ = okx.createSign(HttpMethodGet, BalanceEndpoint)
	log.Debug("Having test signature: ", sign)
}
