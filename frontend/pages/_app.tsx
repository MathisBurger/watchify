import type { AppProps } from 'next/app'
import React from 'react';
import Head from 'next/head';
import '../styles/globals.scss';

function MyApp({ Component, pageProps }: AppProps) {
  return (
      <React.Fragment>
        <Head>
          <link rel="preconnect" href="https://fonts.googleapis.com" />
          <link rel="preconnect" href="https://fonts.gstatic.com" />
          <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@500&display=swap" rel="stylesheet" />
          <title>Watchify</title>
        </Head>
        <Component {...pageProps} />
      </React.Fragment>
  );
}

export default MyApp
