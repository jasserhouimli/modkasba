module.exports = {
    module: {
      rules: [
        {
          test: /\.ttf$/,
          use: [
            {
              loader: 'file-loader',
              options: {
                name: '[name].[ext]',
                outputPath: 'assets/fonts/',
              },
            },
          ],
        },
      ],
    },
  };
  