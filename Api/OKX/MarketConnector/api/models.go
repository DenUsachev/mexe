package api

type ApiResponse struct {
	Event        string `json:"event"`
	Code         string `json:"code"`
	Msg          string `json:"msg"`
	ConnectionID string `json:"connId"`
}

type RequestArgs struct {
	APIKey     string `json:"apiKey"`
	Passphrase string `json:"passphrase"`
	Timestamp  string `json:"timestamp"`
	Sign       string `json:"sign"`
}

type LoginRequest struct {
	Operation string        `json:"op"`
	Args      []RequestArgs `json:"args"`
}

type BalanceResponse struct {
	Code string `json:"code"`
	Data []struct {
		AdjEq      string `json:"adjEq"`
		BorrowFroz string `json:"borrowFroz"`
		Details    []struct {
			AvailBal      string `json:"availBal"`
			AvailEq       string `json:"availEq"`
			BorrowFroz    string `json:"borrowFroz"`
			CashBal       string `json:"cashBal"`
			Ccy           string `json:"ccy"`
			CrossLiab     string `json:"crossLiab"`
			DisEq         string `json:"disEq"`
			Eq            string `json:"eq"`
			EqUsd         string `json:"eqUsd"`
			FixedBal      string `json:"fixedBal"`
			FrozenBal     string `json:"frozenBal"`
			Imr           string `json:"imr"`
			Interest      string `json:"interest"`
			IsoEq         string `json:"isoEq"`
			IsoLiab       string `json:"isoLiab"`
			IsoUpl        string `json:"isoUpl"`
			Liab          string `json:"liab"`
			MaxLoan       string `json:"maxLoan"`
			MgnRatio      string `json:"mgnRatio"`
			Mmr           string `json:"mmr"`
			NotionalLever string `json:"notionalLever"`
			OrdFrozen     string `json:"ordFrozen"`
			SpotInUseAmt  string `json:"spotInUseAmt"`
			SpotIsoBal    string `json:"spotIsoBal"`
			StgyEq        string `json:"stgyEq"`
			Twap          string `json:"twap"`
			UTime         string `json:"uTime"`
			Upl           string `json:"upl"`
			UplLiab       string `json:"uplLiab"`
		} `json:"details"`
		Imr         string `json:"imr"`
		IsoEq       string `json:"isoEq"`
		MgnRatio    string `json:"mgnRatio"`
		Mmr         string `json:"mmr"`
		NotionalUsd string `json:"notionalUsd"`
		OrdFroz     string `json:"ordFroz"`
		TotalEq     string `json:"totalEq"`
		UTime       string `json:"uTime"`
		Upl         string `json:"upl"`
	} `json:"data"`
	Msg string `json:"msg"`
}
