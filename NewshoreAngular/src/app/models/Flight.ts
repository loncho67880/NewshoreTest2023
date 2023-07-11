import { Transport } from "./Transport";

export interface Flight {
    origin: string;
    destination: string;
    price: number;
    transport: Transport;
  }
  