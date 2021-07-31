module.exports = {
    plugins: ["jsx-control-statements"],
    presets: [
        ['@babel/preset-env', { targets: { node: 'current' } }],
        '@babel/preset-typescript',
    ],
};
