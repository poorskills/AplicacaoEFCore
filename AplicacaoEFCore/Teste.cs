namespace TaskAndThread
{
    internal class CalculosLongos
    {
        public async Task<int> CalcularNumeroGrande()
        {
                for (int i = 0; i < 10000; i++)
                {
                    //faça algo demorado aqui
                }
                return await Task.FromResult(0);
        }

    }
}