using System;

public class Class1
{
	public Class1()
	{
        for (j = 0; j < (m_terrainHeight - 1); j++)
        {
            for (i = 0; i < (m_terrainWidth - 1); i++)
            {
                index1 = (m_terrainHeight * j) + i;          // Bottom left.
                index2 = (m_terrainHeight * j) + (i + 1);      // Bottom right.
                index3 = (m_terrainHeight * (j + 1)) + i;      // Upper left.
                index4 = (m_terrainHeight * (j + 1)) + (i + 1);  // Upper right.

                // Upper left.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index3].x, m_heightMap[index3].y, m_heightMap[index3].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Upper right.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index4].x, m_heightMap[index4].y, m_heightMap[index4].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Upper right.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index4].x, m_heightMap[index4].y, m_heightMap[index4].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Bottom left.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index1].x, m_heightMap[index1].y, m_heightMap[index1].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Bottom left.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index1].x, m_heightMap[index1].y, m_heightMap[index1].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Upper left.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index3].x, m_heightMap[index3].y, m_heightMap[index3].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Bottom left.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index1].x, m_heightMap[index1].y, m_heightMap[index1].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Upper right.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index4].x, m_heightMap[index4].y, m_heightMap[index4].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Upper right.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index4].x, m_heightMap[index4].y, m_heightMap[index4].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Bottom right.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index2].x, m_heightMap[index2].y, m_heightMap[index2].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Bottom right.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index2].x, m_heightMap[index2].y, m_heightMap[index2].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;

                // Bottom left.
                vertices[index].position = D3DXVECTOR3(m_heightMap[index1].x, m_heightMap[index1].y, m_heightMap[index1].z);
                vertices[index].color = D3DXVECTOR4(1.0f, 1.0f, 1.0f, 1.0f);
                indices[index] = index;
                index++;
            }
        }
	}
}
