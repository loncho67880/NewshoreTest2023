import { Flight } from "./Flight";

export interface Journey {
    origin: string;
    destination: string;
    price: number;
    srcimage: string;
    flights: Flight[];
  }