import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import "bootstrap/dist/css/bootstrap.css";
import "./index.css";
import 'react-toastify/dist/ReactToastify.css';

import { makeServer } from "./serverMock"

if (process.env.REACT_APP_USE_MOCK_SERVER === "true") {
  makeServer({ environment: "development" })
}

ReactDOM.render(<App />, document.getElementById("root"));
