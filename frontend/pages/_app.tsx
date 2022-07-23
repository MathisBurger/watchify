import type { AppProps } from 'next/app'
import React, {useEffect} from 'react';
import Head from 'next/head';
import '../styles/globals.scss';
import {SnackbarProvider} from "notistack";
import Navbar from "../components/Navbar";

function MyApp({ Component, pageProps }: AppProps) {
    
    useEffect(() => {
        const body = document.querySelector('body');
        if (body) {
            body.style.margin = '0';
            body.style.padding = '0';
        }
    })
  return (
      <React.Fragment>
        <Head>
          <link rel="preconnect" href="https://fonts.googleapis.com" />
          <link rel="preconnect" href="https://fonts.gstatic.com" />
          <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@500&display=swap" rel="stylesheet" />
          <title>Watchify</title>
        </Head>
          <SnackbarProvider>
              <Navbar />
              <Component {...pageProps} />
          </SnackbarProvider>
      </React.Fragment>
  );
}

export default MyApp
