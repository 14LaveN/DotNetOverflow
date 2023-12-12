import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import Identity from "./components/Identity";
import Questions from "./components/Questions";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: "/identity",
    element: <Identity/>
  },
  {
    path: "/questions",
    element: <Questions/>
  }
];

export default AppRoutes;
